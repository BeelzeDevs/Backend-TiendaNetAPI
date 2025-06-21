using TiendaNetApi.Usuario.DTOs;
namespace TiendaNetApi.Usuario.DTOs
{
    public class UsuarioReadConDetallesDTO()
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public int CodPostal { get; set; }
        public bool EstadoUsuario { get; set; } = true;
        public DateTime FechaCreacion { get; set; }

        public RolReadDTO Rol { get; set; } = null!;


    }
}