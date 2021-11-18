using Microsoft.Extensions.Configuration;
using Serilog;
using StackExchange.Redis;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Benivo.Demo.Cache
{
    internal static class RedisHelper
    {

        // In general, let StackExchange.Redis handle most reconnects, 
        // so limit the frequency of how often this will actually reconnect.
        private static readonly TimeSpan _reconnectMinFrequency;

        // if errors continue for longer than the below threshold, then the 
        // multiplexer seems to not be reconnecting, so re-create the multiplexer
        private static readonly TimeSpan _reconnectErrorThreshold;

        private static readonly object _reconnectLock;
        private static readonly string _connectionString;

        private static Lazy<ConnectionMultiplexer> _multiplexer;
        private static long _lastReconnectTicks;
        private static DateTimeOffset _firstError;
        private static DateTimeOffset _previousError;

        static RedisHelper()
        {
            _connectionString = GetConnectionString();
            _reconnectLock = new object();
            _multiplexer = CreateMultiplexer();
            _lastReconnectTicks = DateTimeOffset.MinValue.UtcTicks;
            _firstError = DateTimeOffset.MinValue;
            _previousError = DateTimeOffset.MinValue;

            _reconnectMinFrequency = TimeSpan.FromSeconds(60);
            _reconnectErrorThreshold = TimeSpan.FromSeconds(30);
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot Configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            return Configuration.GetConnectionString("Redis");
        }

        private static (string Host, int Port) GetHostAndPort()
        {
            IConfigurationRoot Configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

            var redisConfigurations = Configuration.GetSection("RedisHostAndPort");

            return new(redisConfigurations.GetValue<string>("Host"),
                redisConfigurations.GetValue<int>("Port"));
        }

        /// <summary>
        /// Force a new ConnectionMultiplexer to be created.  
        /// NOTES: 
        ///     1. Users of the ConnectionMultiplexer MUST handle ObjectDisposedExceptions, which can now happen as a result of calling ForceReconnect()
        ///     2. Don't call ForceReconnect for Timeouts, just for RedisConnectionExceptions or SocketExceptions
        ///     3. Call this method every time you see a connection exception, the code will wait to reconnect:
        ///         a. for at least the "ReconnectErrorThreshold" time of repeated errors before actually reconnecting
        ///         b. not reconnect more frequently than configured in "ReconnectMinFrequency"
        /// </summary>    
        public static void ForceReconnect()
        {
            var utcNow = DateTimeOffset.UtcNow;
            var previousTicks = Interlocked.Read(ref _lastReconnectTicks);
            var previousReconnect = new DateTimeOffset(previousTicks, TimeSpan.Zero);
            var elapsedSinceLastReconnect = utcNow - previousReconnect;

            // If mulitple threads call ForceReconnect at the same time, we only want to honor one of them.
            if (elapsedSinceLastReconnect > _reconnectMinFrequency)
            {
                lock (_reconnectLock)
                {
                    utcNow = DateTimeOffset.UtcNow;
                    elapsedSinceLastReconnect = utcNow - previousReconnect;

                    if (_firstError == DateTimeOffset.MinValue)
                    {
                        // We haven't seen an error since last reconnect, so set initial values.
                        _firstError = utcNow;
                        _previousError = utcNow;
                        return;
                    }

                    if (elapsedSinceLastReconnect < _reconnectMinFrequency)
                        return; // Some other thread made it through the check and the lock, so nothing to do.

                    var elapsedSinceFirstError = utcNow - _firstError;
                    var elapsedSinceMostRecentError = utcNow - _previousError;

                    var shouldReconnect =
                        elapsedSinceFirstError >= _reconnectErrorThreshold   // make sure we gave the multiplexer enough time to reconnect on its own if it can
                        && elapsedSinceMostRecentError <= _reconnectErrorThreshold; //make sure we aren't working on stale data (e.g. if there was a gap in errors, don't reconnect yet).

                    // Update the previousError timestamp to be now (e.g. this reconnect request)
                    _previousError = utcNow;

                    if (shouldReconnect)
                    {
                        _firstError = DateTimeOffset.MinValue;
                        _previousError = DateTimeOffset.MinValue;

                        var oldMultiplexer = _multiplexer;
                        CloseMultiplexer(oldMultiplexer);
                        _multiplexer = CreateMultiplexer();

                        Interlocked.Exchange(ref _lastReconnectTicks, utcNow.UtcTicks);
                    }
                }
            }
        }

        private static Lazy<ConnectionMultiplexer> CreateMultiplexer()
        {
            Lazy<ConnectionMultiplexer> connectionMultiplexer = null;

            try
            {
                connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_connectionString));
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return connectionMultiplexer;
        }

        private static void CloseMultiplexer(Lazy<ConnectionMultiplexer> oldMultiplexer)
        {
            if (oldMultiplexer != null)
            {
                try
                {
                    oldMultiplexer.Value.Close();
                }
                catch (Exception)
                {
                    // Example error condition: if accessing old.Value causes a connection attempt and that fails.
                }
            }
        }

        #region public extensions
        public static IDatabase TryGetDatabase()
        {
            IDatabase database = default;

            try
            {
                database = _multiplexer.Value.GetDatabase();
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return database;
        }

        public static IServer TryGetServer()
        {
            IServer server = default;

            try
            {
                var (Host, Port) = GetHostAndPort();

                server = _multiplexer.Value.GetServer(Host, Port);
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return server;
        }
        #endregion
    }
}
