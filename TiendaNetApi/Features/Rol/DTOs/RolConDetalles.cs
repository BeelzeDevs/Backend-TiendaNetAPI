using TiendaNetApi.Rol.DTOs;

namespace TiendaNetApi.Rol.DTOs
{
    public class RolConDetallesDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public List<UsuarioRolDetalleDTO > Usuarios { get; set; } = new();
    }
}