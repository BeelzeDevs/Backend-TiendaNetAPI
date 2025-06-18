using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{

    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Municipio { get; set; } = null!;
        public int CodPostal { get; set; }
        public string Contraseña { get; set; } = null!;  // En producción deberíamos hashear
        public bool EstadoUsuario { get; set; } = true;
        public DateTime FechaCreacion { get; set; }

        public int RolId { get; set; } // Clave foránea explícita
        public Rol Rol { get; set; } = null!;
    }

}