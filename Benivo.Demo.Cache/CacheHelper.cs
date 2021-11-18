using Newtonsoft.Json;
using Serilog;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Benivo.Demo.Cache
{
    public static class CacheHelper
    {
        private static readonly ConcurrentDictionary<object, SemaphoreSlim> _locks;

        static CacheHelper() => _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        private const string _baseKey = "benivo_demo_";
        public static async Task<TItem> GetOrSetAsync<TItem>(string key, Func<Task<TItem>> createItem, TimeSpan expiration)
        {
            IDatabase redisDb = RedisHelper.TryGetDatabase();
            key = GetCacheKey(key);
            TItem cacheValue = default;

            RedisValue redisValue = await redisDb.TryGetAsync(key);

            if (!redisValue.HasValue)
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();

                try
                {
                    redisValue = await redisDb.TryGetAsync(key);
                    if (!redisValue.HasValue)
                    {
                        cacheValue = await createItem();
                        await redisDb.TrySetAsync(key, cacheValue, expiration);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, ex.Message);
                }
                finally
                {
                    mylock.Release();
                }
            }

            if (redisValue.HasValue)
                cacheValue = JsonConvert.DeserializeObject<TItem>(redisValue);

            return cacheValue;
        }

        private static string GetCacheKey(string cacheKey) => $"{_baseKey}{cacheKey}";
    }
}
