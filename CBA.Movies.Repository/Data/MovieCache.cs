using System;
using System.Runtime.Caching;

namespace CBA.MoviesSource.Data
{
    public interface IMovieCache
    {
        void Add(string key, object value, DateTimeOffset absoluteExpiry);
        T Get<T>(string key);
        void Remove(string key);
    }

    public class MovieCache : IMovieCache
    {
        private readonly MemoryCache _memCache;

        public MovieCache()
            : this(MemoryCache.Default)
        {
        }

        public MovieCache(MemoryCache memCache)
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
