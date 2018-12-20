using PhotoSearch.Api.Exceptions;
using System.Net;

namespace PhotoSearch.Api.Authentication
{
    public class AuthenticationException : DenialException
    {
        public AuthenticationException(string errorCode)
            : this(HttpStatusCode.Unauthorized, errorCode) {
        }

        public AuthenticationException(HttpStatusCode httpStatusCode, string errorCode)
            : base(httpStatusCode, errorCode) {
        }

        public AuthenticationException(string errorCode, params object[] parameters)
            : base(errorCode, parameters) {
        }

        public AuthenticationException(HttpStatusCode statusCode, string errorCode, params object[] parameters)
            : base(statusCode, errorCode, parameters) {
        }

        public static AuthenticationException InvalidCredential => new AuthenticationException(HttpStatusCode.BadRequest, "InvalidCredentials");

        public static AuthenticationException MissingCredential => new AuthenticationException(HttpStatusCode.BadRequest, "MissingCredentials");
    }
}
