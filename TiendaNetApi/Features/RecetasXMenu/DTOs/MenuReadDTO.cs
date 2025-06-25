namespace TiendaNetApi.RecetasXMenu.DTOs
{
    public class MenuReadDTO
    {
        public int Id { get; set; }
        public string TituloMenu { get; set; } = string.Empty;
        public bool EstadoMenu { get; set; }

    }
}