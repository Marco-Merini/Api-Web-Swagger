using ApiProdutosPessoas.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ApiProdutosPessoas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        // Usuários fixos para demonstração (em produção deveriam estar no banco de dados)
        private readonly static Dictionary<string, string> _users = new Dictionary<string, string>
        {
            { "admin", "admin123" },
            { "user", "user123" }
        };

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials model)
        {
            // Valida as credenciais do usuário
            if (!_users.TryGetValue(model.Username, out var password) || password != model.Password)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos" });
            }

            // Gera o token
            var token = _tokenService.GenerateToken(model.Username);

            return Ok(token);
        }
    }
}
