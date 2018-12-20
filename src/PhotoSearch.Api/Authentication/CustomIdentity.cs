using System.Security.Principal;

namespace PhotoSearch.Api.Authentication
{
    public class CustomIdentity : IIdentity
    {
        public CustomIdentity(string userName) {
            Name = userName;
        }

        public string AuthenticationType => string.Empty;

        public bool IsAuthenticated => true;

        public string Name { get; }
    }
}
