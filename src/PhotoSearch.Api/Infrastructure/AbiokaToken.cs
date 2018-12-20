using Microsoft.IdentityModel.Tokens;
using PhotoSearch.Api.Authentication;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace PhotoSearch.Api.Infrastructure
{
    public class AbiokaToken : IAbiokaToken
    {
        private const string key = "_A4b%i+oKa$_abioka????";
        private const string issuer = "Abioka";
        private const string aud = "abioka.com";

        public string CreateToken(CustomIdentity identity) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(identity),
                Expires = DateTime.UtcNow.AddDays(7),
                IssuedAt = DateTime.UtcNow,
                Audience = aud,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };

            return tokenHandler.CreateEncodedJwt(tokenDescriptor);
        }

        public IIdentity GetIdentity(string token) {
            var validationParameters = new TokenValidationParameters() {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidIssuer = issuer,
                ValidAudience = aud,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true
            };

            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securitToken;
                var claim = tokenHandler.ValidateToken(token, validationParameters, out securitToken);
                return claim.Identity;
            } catch (SecurityTokenException) {
                throw AuthenticationException.InvalidCredential;
            }
        }
    }
}
