using System;

namespace PhotoSearch.Api.Infrastructure
{
    public interface ICacheable
    {
        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <value>
        /// The cache key.
        /// </value>
        string CacheKey { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        TimeSpan Duration { get; } 
    }
}
