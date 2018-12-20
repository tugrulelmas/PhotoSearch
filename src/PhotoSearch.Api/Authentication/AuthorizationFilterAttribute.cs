using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PhotoSearch.Api.Authentication
{
    /// <summary>
    /// Authorization Filter Attribute
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class AuthorizationFilterAttribute : IOperationFilter
    {
        /// <summary>
        /// Applies the authorization parameters and responses.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(Operation operation, OperationFilterContext context) {
            var allowAnonymousAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                      .Union(context.MethodInfo.GetCustomAttributes(true))
                                      .OfType<AllowAnonymousAttribute>();

            if (!allowAnonymousAttributes.Any()) {
                operation.Description = "Authorization needed";

                if (operation.Parameters == null) {
                    operation.Parameters = new List<IParameter>();
                }

                operation.Parameters.Add(new NonBodyParameter() {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = true,
                    Description = "Bearer token",
                });

                operation.Responses.Add(((int)HttpStatusCode.Unauthorized).ToString(), new Response { Description = "InvalidToken - InvalidPassword - InvalidProvider - TokenHasExpired - InvalidSignature" });
                operation.Responses.Add(((int)HttpStatusCode.BadRequest).ToString(), new Response { Description = "InvalidCredentials - MissingCredentials" });
                operation.Responses.Add(((int)HttpStatusCode.Forbidden).ToString(), new Response { Description = "Forbidden" });
            }
        }
    }
}
