namespace TiendaNetApi.DTOs
{
    public class UsuarioDto()
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Rol { get; set; } = null!;

    }
}