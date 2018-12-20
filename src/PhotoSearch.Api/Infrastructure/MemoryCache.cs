using Microsoft.Extensions.Caching.Memory;
using System;

namespace PhotoSearch.Api.Infrastructure
{
    internal class MemoryCache : ICache
    {
        private readonly IMemoryCache memoryCache;

        public MemoryCache(IMemoryCache memoryCache) {
            this.memoryCache = memoryCache;
        }

        public void Remove(object key) {
            memoryCache.Remove(key);
        }

        public bool TryGetValue<T>(object key, out T value) => memoryCache.TryGetValue(key, out value);

        public T Set<T>(object key, T value) => memoryCache.Set(key, value);

        public T Set<T>(object key, T value, TimeSpan absoluteExpirationRelativeToNow) => memoryCache.Set(key, value, absoluteExpirationRelativeToNow);
    }
}
