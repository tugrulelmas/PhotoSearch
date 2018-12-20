using PhotoSearch.Api.Authentication;
using System.Security.Principal;

namespace PhotoSearch.Api.Infrastructure
{
    public interface IAbiokaToken
    {
        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>
        string CreateToken(CustomIdentity identity);

        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        IIdentity GetIdentity(string token);
    }
}
