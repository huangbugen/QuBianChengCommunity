using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using QubianCheng.CacheManager.IService;
using QuBianCheng.Core.Extensions;

namespace QubianCheng.CacheManager
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            this._cache = cache;
        }

        public bool Exists(string key)
        {
            key.CheckNull();
            return _cache.Get(key) != null;
        }

        public bool Add(string key, object value)
        {
            key.CheckNull();
            value.CheckNull();
            _cache.Set(key, value);
            return Exists(key);
        }
        public bool Add(string key, object value, TimeSpan ts)
        {
            key.CheckNull();
            value.CheckNull();
            _cache.Set(key, value, ts);
            return Exists(key);
        }

        public void Remove(params string[] keys)
        {
            keys.CheckNull();
            foreach (var item in keys)
            {
                _cache.Remove(item);
            }

        }

        public string Get(string key)
        {
            return _cache.Get(key)?.ToString();
        }
        public T Get<T>(string key) where T : class
        {
            var value = _cache.Get(key) as T;
            return value;
        }

        public void Dispose()
        {
            if (_cache != null)
                _cache.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool AddHash(string key, Dictionary<string, object> fieldValus)
        {
            return false;
        }

        public bool AddHash(string key, Dictionary<string, object> fieldValus, TimeSpan? ts)
        {
            return false;
        }
    }
}