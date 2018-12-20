using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoSearch.Api.Authentication;
using PhotoSearch.Api.Exceptions;
using PhotoSearch.Api.Infrastructure;

namespace PhotoSearch.Api.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    /// <seealso cref="ApiController" />
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IAbiokaToken abiokaToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="abiokaToken"></param>
        public AuthController(IAbiokaToken abiokaToken) {
            this.abiokaToken = abiokaToken;
        }


        /// <summary>
        /// Creates new JWT.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns></returns>
        /// <exception cref="DenialException"></exception>
        [AllowAnonymous]
        [HttpPost("")]
        public ActionResult<string> Login([FromQuery]string userName) {
            var token = abiokaToken.CreateToken(new CustomIdentity(userName));
            return Ok(token);
        }
    }
}
