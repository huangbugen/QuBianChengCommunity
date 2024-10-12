using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QubianCheng.CacheManager.IService
{
    public interface ICacheService : IDisposable
    {
        bool Add(string key, object value, TimeSpan ts);
        bool Add(string key, object value);
        bool AddHash(string key, Dictionary<string, object> fieldValus);
        bool AddHash(string key, Dictionary<string, object> fieldValus, TimeSpan? ts);
        void Dispose();
        bool Exists(string key);
        string Get(string key);
        T Get<T>(string key) where T : class;
        void Remove(params string[] keys);
    }
}