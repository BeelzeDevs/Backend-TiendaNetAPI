using TiendaNetApi.Model;
namespace TiendaNetApi.Model
{
    public class UnidadMedida
    {
        public int Id{ get; set; }
        public string Nombre { get; set; } = null!;

        public ICollection<Model.Ingrediente> Ingredientes { get; set; } = new List<Model.Ingrediente>();
    }
}