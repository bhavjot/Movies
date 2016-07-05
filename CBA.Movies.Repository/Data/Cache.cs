using System;
using System.Runtime.Caching;
using CBA.Framework.Autofac.Attributes;
using CBA.Framework.Autofac.Enums;

namespace CBA.Movies.Repository.Data
{
    public interface ICache
    {
        void Add(string key, object value, DateTimeOffset absoluteExpiry);
        T Get<T>(string key);
        void Remove(string key);
    }

    [AutoWired(LifetimeScope.SingleInstance)]
    public class Cache : ICache
    {
        private readonly MemoryCache _memCache;

        public Cache()
            : this(MemoryCache.Default)
        {
        }

        public Cache(MemoryCache memCache)
        {
            _memCache = memCache;
        }
        public T Get<T>(string key)
        {
            return (T)_memCache.Get(key);
        }

        public void Add(string key, object value, DateTimeOffset absoluteExpiry)
        {
            _memCache.Add(key, value, absoluteExpiry);
        }

        public void Remove(string key)
        {
            _memCache.Remove(key);
        }

        
    }
}
