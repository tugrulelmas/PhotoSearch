using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using PhotoSearch.Api.Authentication;
using PhotoSearch.Api.Infrastructure;
using System.Linq;

namespace PhotoSearch.Api.ActionDecorators
{
    public class BearerAuthenticationDecorator : IActionBeforeDecorator
    {
        private readonly IAbiokaToken abiokaToken;

        public BearerAuthenticationDecorator(IAbiokaToken abiokaToken) {
            this.abiokaToken = abiokaToken;
        }

        public void Decorate(ActionExecutingContext context) {
            var isAllowAnonymous = context.Filters.Any(f => f.GetType() == typeof(AllowAnonymousFilter));
            if (isAllowAnonymous) {
                return;
            }

            var auth = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(auth))
                throw AuthenticationException.MissingCredential;

            var parameters = auth.Split(' ');
            if (parameters.Length != 2 || string.IsNullOrWhiteSpace(parameters[1]) || parameters[0] != "Bearer")
                throw AuthenticationException.MissingCredential;

            var token = parameters[1];
            var identity = abiokaToken.GetIdentity(token);
        }
    }
}
