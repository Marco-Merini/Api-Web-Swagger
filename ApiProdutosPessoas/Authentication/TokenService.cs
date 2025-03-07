using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Authentication
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserToken GenerateToken(string username)
        {
            // Lê as configurações do JWT do appsettings.json
            var jwtSecret = _configuration["JWT:Secret"];
            var jwtIssuer = _configuration["JWT:ValidIssuer"];
            var jwtAudience = _configuration["JWT:ValidAudience"];
            var expiryInMinutes = Convert.ToDouble(_configuration["JWT:ExpiryInMinutes"]);

            // Cria as claims para o token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            // Cria a chave de criptografia para assinar o token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define quando o token expira
            var expiry = DateTime.Now.AddMinutes(expiryInMinutes);

            // Cria o token
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            // Retorna o token e informações adicionais
            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = username,
                ExpiresIn = expiryInMinutes * 60
            };
        }
    }
}
