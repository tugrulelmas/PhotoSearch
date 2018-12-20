using FluentValidation;
using MediatR;
using PhotoSearch.Api.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoSearch.Api.Pipelines
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> validatior;

        public ValidationPipelineBehavior(IValidator<TRequest> validatior) {
            this.validatior = validatior;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            if (validatior == null) {
                return await next();
            }

            var validationResult = validatior.Validate(request);
            if (validationResult.IsValid) {
                return await next();
            }

            // TODO: throw exception for all error messages
            var firstError = validationResult.Errors.First();
            throw new DenialException(firstError.ErrorMessage, firstError.PropertyName);
        }
    }
}
