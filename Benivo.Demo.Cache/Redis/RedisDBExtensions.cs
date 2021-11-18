using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Benivo.Demo.Cache
{
    internal static class RedisDBExtensions
    {
        public static bool TrySet(this IDatabase db, RedisKey key, object value, TimeSpan expiration)
        {
            bool result = false;

            if (db == default)
                return result;

            string stringValue;

            if (value.GetType() != typeof(string))
                stringValue = SerializeObjectWithRefLoopIgnore(value);
            else
                stringValue = value.ToString();

            try
            {
                result = db.StringSet(key, stringValue, expiration);
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    RedisHelper.ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return result;
        }

        public static bool TryGet(this IDatabase db, RedisKey key, out RedisValue redisValue)
        {
            bool result = false;
            redisValue = default;

            if (db == default)
                return result;

            try
            {
                redisValue = db.StringGet(key);
                result = redisValue.HasValue;
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    RedisHelper.ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return result;
        }

        public static bool TryRemove(this IDatabase db, RedisKey key)
        {
            bool result = false;

            if (db == default)
                return result;

            try
            {
                result = db.KeyDelete(key);
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    RedisHelper.ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return result;
        }

        public static async Task<bool> TrySetAsync(this IDatabase db, RedisKey key, object value, TimeSpan expiration)
        {
            bool result = false;

            if (db == default)
                return result;

            string stringValue;

            if (value.GetType() != typeof(string))
                stringValue = SerializeObjectWithRefLoopIgnore(value);
            else
                stringValue = value.ToString();

            try
            {
                result = await db.StringSetAsync(key, stringValue, expiration);
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    RedisHelper.ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return result;
        }

        public static async Task<RedisValue> TryGetAsync(this IDatabase db, string key)
        {
            RedisValue redisValue = default;

            if (db == default)
                return redisValue;

            try
            {
                redisValue = await db.StringGetAsync(key);
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    RedisHelper.ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return redisValue;
        }

        public static async Task<bool> TryRemoveAsync(this IDatabase db, string key)
        {
            bool result = false;

            if (db == default)
                return result;

            try
            {
                result = await db.KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                if (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                    RedisHelper.ForceReconnect();

                Log.Error(ex, ex.Message);
            }

            return result;
        }

        private static string SerializeObjectWithRefLoopIgnore(object value)
            => JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
    }
}
