using ContaBancaria_API.Models;
using ContaBancaria_API.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContaBancaria_API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var secretKey = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("A chave JWT (Jwt:Key) não foi configurada no appsettings ou variáveis de ambiente.");
            }

            var key = Encoding.ASCII.GetBytes(secretKey);

            var expiresMinutes = _config.GetValue<int>("Jwt:ExpiresInMinutes");
            if (expiresMinutes == 0) expiresMinutes = 30;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),

            }),
                Expires = DateTime.UtcNow.AddHours(expiresMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
