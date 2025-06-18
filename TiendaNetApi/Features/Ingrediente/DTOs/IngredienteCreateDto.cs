namespace TiendaNetApi.Ingredientes.DTOs
{
    public class IngredienteCreateDTO
    {
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public decimal Stock { get; set; }
        public string? DescripcionIngrediente { get; set; }
        public int UnidadMedidaId{ get; set; }
    }
}