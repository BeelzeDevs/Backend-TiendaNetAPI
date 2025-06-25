using TiendaNetApi.Auth.DTOs;

namespace TiendaNetApi.Auth.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO?> LoginAsync(UsuarioLoginDTO dto);
    }
}