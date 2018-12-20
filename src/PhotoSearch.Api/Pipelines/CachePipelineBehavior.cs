using MediatR;
using PhotoSearch.Api.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Pipelines
{
    public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICache cache;

        public CachePipelineBehavior(ICache cache) {
            this.cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            var cacheable = request as ICacheable;
            if (cacheable == null) {
                return await next();
            }

            TResponse result;
            if (!cache.TryGetValue(cacheable.CacheKey, out result)) {
                result = await next();
                cache.Set(cacheable.CacheKey, result, cacheable.Duration);
            }

            return result;
        }
    }
}
