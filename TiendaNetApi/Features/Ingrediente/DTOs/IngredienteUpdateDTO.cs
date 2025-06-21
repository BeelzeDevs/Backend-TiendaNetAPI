namespace TiendaNetApi.Ingredientes.DTOs
{
    public class IngredienteUpdateDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Costo { get; set; }
        public decimal Stock { get; set; }
        public string? DescripcionIngrediente { get; set; }
        public int UnidadMedidaId { get; set; }
        public bool EstadoIngrediente { get; set; } = true;
    }
}