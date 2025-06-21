namespace TiendaNetApi.Receta.DTOs
{
    public class IngredienteDetalleDTO
    {
        public int IngredienteId { get; set; }
        public string NombreIngrediente { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public string UnidadMedidaNombre  { get; set; } = string.Empty;
    }
}