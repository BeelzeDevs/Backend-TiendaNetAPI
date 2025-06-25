namespace TiendaNetApi.Auth.DTOs
{
    public class UsuarioLoginDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UsuarioTokenDTO
    {
        public string Token { get; set; } = string.Empty;
    }
}