using System;
using System.Runtime.Caching;

namespace #projectname#.Helpers
{
    public class MemoryCacheManager
    {
        ObjectCache cache;

        public MemoryCacheManager()
        {
            cache = MemoryCache.Default;
        }

        public void Add<T>(string key, T source)
        {
            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60) };
            cache.Add(key, source, policy);
        }

        public bool Contains(string key)
        {
            return cache.Contains(key);
        }

        public T Get<T>(string key)
        {
            return (T)cache.Get(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}