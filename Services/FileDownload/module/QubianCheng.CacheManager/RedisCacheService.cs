using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSRedis;
using FileDownLoadSystem.Core.Configuration;
using QubianCheng.CacheManager.IService;
using QuBianCheng.Core.Extensions;

namespace QubianCheng.CacheManager
{
    public class RedisCacheService : ICacheService
    {
        public RedisCacheService()
        {
            var csredis = new CSRedisClient(null,AppSetting.RedisConnectionString);
            RedisHelper.Initialization(csredis);
        }
        public bool Add(string key, object value, TimeSpan ts)
        {
            // var val = RedisHelper.RPush("lpush","ace","jack","taro");
            return RedisHelper.Set(key, value, ts);
            // return true;
        }

        public bool Add(string key, object value)
        {
            return RedisHelper.Set(key, value);
        }

        public bool AddHash(string key, Dictionary<string, object> fieldValus)
        {
            return AddHash(key, fieldValus, null);
        }

        public bool AddHash(string key, Dictionary<string, object> fieldValus, TimeSpan? ts)
        {
            List<object> oKeyValus = new();
            fieldValus.CheckDictionaryNull();
            foreach (var keyValue in fieldValus)
            {
                oKeyValus.Add(keyValue.Key);
                oKeyValus.Add(keyValue.Value);
            }
            RedisHelper.HMSet(key, oKeyValus.ToArray());
            // if (ts != null)
            if (ts.HasValue)
            {
                RedisHelper.Expire(key, ts.Value);
            }
            return true;
        }

        public void Dispose()
        {
        }

        public bool Exists(string key)
        {
            key.CheckNull();
            return RedisHelper.Exists(key);
        }

        public string Get(string key)
        {
            return RedisHelper.Get(key);
        }

        public T Get<T>(string key) where T : class
        {
            return RedisHelper.Get<T>(key);
        }

        public void Remove(params string[] keys)
        {
            RedisHelper.Del(keys);
        }
    }
}