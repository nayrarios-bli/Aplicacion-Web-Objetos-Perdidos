using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ObjetosPerdidosUCB.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ObjetosPerdidosUCB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public TokenController(
            IConfiguration configuration,
            IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication([FromBody] UserLogin userLogin)
        {
            if (string.IsNullOrWhiteSpace(userLogin.User) ||
                string.IsNullOrWhiteSpace(userLogin.Password))
            {
                return BadRequest(new
                {
                    message = "El correo y la contraseña son obligatorios."
                });
            }

            // El campo User recibe el correo electrónico.
            var usuario = await _usuarioService
                .GetUsuarioByCorreoAsync(userLogin.User.Trim());

            if (usuario == null ||
                !usuario.Estado ||
                !BCrypt.Net.BCrypt.Verify(
                    userLogin.Password,
                    usuario.Contrasena))
            {
                return Unauthorized(new
                {
                    message = "Credenciales inválidas."
                });
            }

            var token = GenerateToken(usuario);
            return Ok(new { token });
        }

        private string GenerateToken(Usuario usuario)
        {
            var secretKey = _configuration["Authentication:SecretKey"];
            var issuer = _configuration["Authentication:Issuer"];
            var audience = _configuration["Authentication:Audience"];

            if (string.IsNullOrWhiteSpace(secretKey) ||
                string.IsNullOrWhiteSpace(issuer) ||
                string.IsNullOrWhiteSpace(audience))
            {
                throw new InvalidOperationException(
                    "Falta configurar la clave, el emisor o la audiencia del token.");
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    usuario.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    usuario.Nombre),

                new Claim(
                    ClaimTypes.Email,
                    usuario.CorreoElectronico),

                new Claim(
                    ClaimTypes.Role,
                    usuario.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}