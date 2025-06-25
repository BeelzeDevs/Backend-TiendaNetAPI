namespace TiendaNetApi.Auth.DTOs
{
    public class AuthResponseDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiraEn { get; set; }
    }
}