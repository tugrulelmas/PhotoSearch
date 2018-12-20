using System.Net;

namespace PhotoSearch.Api.Exceptions
{
    public class NotFoundException : DenialException
    {
        public NotFoundException(params object[] parameters)
            : base(HttpStatusCode.NotFound, "NotFound", parameters) {
        }
    }
}