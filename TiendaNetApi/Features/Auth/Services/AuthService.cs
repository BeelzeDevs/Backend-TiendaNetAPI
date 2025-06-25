using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TiendaNetApi.Auth.DTOs;
using TiendaNetApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TiendaNetApi.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly TiendaDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(TiendaDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<AuthResponseDTO?> LoginAsync(UsuarioLoginDTO dto)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario && u.Contraseña == dto.Password);

            if (usuario is null)
                return null;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol.Nombre),
                new Claim("UsuarioId", usuario.Id.ToString())
            };

            var expiration = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new AuthResponseDTO
            {
                NombreUsuario = usuario.NombreUsuario,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiraEn = expiration
            };
        }
    }
}