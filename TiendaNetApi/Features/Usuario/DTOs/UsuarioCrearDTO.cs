namespace TiendaNetApi.Usuario.DTOs
{
    public class UsuarioCrearDTO
    {
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public int CodPostal { get; set; }
        public string Contraseña { get; set; } = string.Empty;
        public int RolId { get; set; } = 2; // Cliente, no admin
    }
}