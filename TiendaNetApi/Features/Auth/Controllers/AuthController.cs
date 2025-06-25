using Microsoft.AspNetCore.Mvc;
using TiendaNetApi.Auth.DTOs;
using TiendaNetApi.Auth.Services;

namespace TiendaNetApi.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDTO dto)
        {
            var authResult = await _authService.LoginAsync(dto);

            return authResult is not null
                ? Ok(authResult)
                : Unauthorized("Nombre de usuario o contraseña incorrectos.");
        }
    }
}
