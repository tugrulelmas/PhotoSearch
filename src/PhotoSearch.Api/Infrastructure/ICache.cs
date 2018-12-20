using System;

namespace PhotoSearch.Api.Infrastructure
{
    public interface ICache
    {
        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(object key);

        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool TryGetValue<T>(object key, out T value);

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteExpirationRelativeToNow">The absolute expiration relative to now.</param>
        /// <returns></returns>
        T Set<T>(object key, T value, TimeSpan absoluteExpirationRelativeToNow);
    }
}
