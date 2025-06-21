namespace TiendaNetApi.Rol.DTOs
{
    public class UsuarioRolDetalleDTO 
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public int CodPostal { get; set; }
        public bool EstadoUsuario { get; set; }

    }
}