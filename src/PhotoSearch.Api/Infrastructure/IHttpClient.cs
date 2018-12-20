using System.Threading.Tasks;

namespace PhotoSearch.Api.Infrastructure
{
    public interface IHttpClient
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string requestUri);
    }
}
