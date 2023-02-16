using Microsoft.IdentityModel.Tokens;
using PawfectMatch.DataLayer;
using PawfectMatch.JwtIssuer.Interface;
using PawfectMatch.JwtIssuer.JwtClaims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PawfectMatch.JwtIssuer
{
    public class JwtIssuerManager : IJwtIssuerManager
    {
        public JwtIssuerManager(IJwtConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IJwtConfiguration Configuration { get; }

        public string GenerateAuthToken(ApplicationUser applicationUser)
        {
            var issuer = Configuration.Issuer;
            var audience = Configuration.Audience;
            var key = Encoding.ASCII.GetBytes(Configuration.EncryptionKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtClaimsNames.UserId, applicationUser.Id.ToString()),
                new Claim(JwtClaimsNames.UserName, applicationUser.UserName),
                new Claim(JwtClaimsNames.Email, applicationUser.Password),
                new Claim(JwtClaimsNames.Jti,Guid.NewGuid().ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}