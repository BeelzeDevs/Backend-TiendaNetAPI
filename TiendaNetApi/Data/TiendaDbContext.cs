using Microsoft.EntityFrameworkCore;
using TiendaNetApi.Model;
namespace TiendaNetApi.Data
{
    public class TiendaDbContext : DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options)
        {
        }
        // Tengo que llamarlos de esta manera por usar Rol,etc Como namespace. Para usar un modelo lo más probable es que tenga de invocarlo de esta manera no tan ambigua
        public DbSet<TiendaNetApi.Model.Rol> Roles { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.Usuario> Usuarios { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.UnidadMedida> UnidadesMedida { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.Ingrediente> Ingredientes { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.Receta> Recetas { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.IngredienteXReceta> IngredientesXRecetas { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.Menu> Menus { get; set; } = null!;
        public DbSet<TiendaNetApi.Model.RecetaXMenu> RecetasXMenu { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.IngredienteXReceta>()
                .HasKey(ixr => new { ixr.IngredienteId, ixr.RecetaId });
            modelBuilder.Entity<Model.RecetaXMenu>()
                .HasKey(rxm => new { rxm.RecetaId, rxm.MenuId });

        }

    }
}