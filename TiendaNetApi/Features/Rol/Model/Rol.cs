using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public ICollection<Model.Usuario> Usuarios { get; set; } = new List<Model.Usuario>();
    }

}