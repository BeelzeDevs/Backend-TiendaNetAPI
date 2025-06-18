using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Model;
namespace TiendaNetApi.Data
{
    public class TiendaDbContext : DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<UnidadMedida> UnidadesDeMedida { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.Ingrediente> Ingredientes { get; set; } = null!; // Tenía un error del linter namespace, por eso utilice el Model.Ingrediente
        public DbSet<Receta> Recetas { get; set; } = null!;
        public DbSet<IngredienteXReceta> IngredientesXRecetas { get; set; } = null!;
        public DbSet<Menu> Menus { get; set; } = null!;
        public DbSet<RecetaXMenu> RecetasXMenu { get; set; } = null!;
    }
}