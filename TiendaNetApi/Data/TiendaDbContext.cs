using Microsoft.EntityFrameworkCore;
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


            // Configurar nombres en minúsculas para postgres. Todo nombre se encuentra en minúscula

            modelBuilder.Entity<TiendaNetApi.Model.Rol>(entity =>
            {
                entity.ToTable("roles");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
            });
            modelBuilder.Entity<TiendaNetApi.Model.Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NombreUsuario).HasColumnName("nombreusuario");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Direccion).HasColumnName("direccion");
                entity.Property(e => e.Municipio).HasColumnName("municipio");
                entity.Property(e => e.CodPostal).HasColumnName("codpostal");
                entity.Property(e => e.Contraseña).HasColumnName("contraseña");
                entity.Property(e => e.RolId).HasColumnName("rolid");
                entity.Property(e => e.EstadoUsuario).HasColumnName("estadousuario");
                entity.Property(e => e.FechaCreacion).HasColumnName("fechacreacion");
            });
            modelBuilder.Entity<TiendaNetApi.Model.UnidadMedida>(entity =>
            {
                entity.ToTable("unidadesmedida");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
            });
            modelBuilder.Entity<TiendaNetApi.Model.Ingrediente>(entity =>
            {
                entity.ToTable("ingredientes");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Costo).HasColumnName("costo");
                entity.Property(e => e.Stock).HasColumnName("stock");
                entity.Property(e => e.DescripcionIngrediente).HasColumnName("descripcioningrediente");
                entity.Property(e => e.EstadoIngrediente).HasColumnName("estadoingrediente");
                entity.Property(e => e.UnidadMedidaId).HasColumnName("unidadmedidaid");
            });
            modelBuilder.Entity<TiendaNetApi.Model.Receta>(entity =>
            {
                entity.ToTable("recetas");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.DescripcionReceta).HasColumnName("descripcionreceta");
                entity.Property(e => e.ImgUrl).HasColumnName("imgurl");
                entity.Property(e => e.PrecioReceta).HasColumnName("precioreceta");
                entity.Property(e => e.EstadoReceta).HasColumnName("estadoreceta");
            });
            modelBuilder.Entity<TiendaNetApi.Model.IngredienteXReceta>(entity =>
            {
                entity.ToTable("ingredientexreceta");
                entity.HasKey(e => new { e.IngredienteId, e.RecetaId });

                entity.Property(e => e.IngredienteId).HasColumnName("ingredienteid");
                entity.Property(e => e.RecetaId).HasColumnName("recetaid");
                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            });
            modelBuilder.Entity<TiendaNetApi.Model.Menu>(entity =>
            {
                entity.ToTable("menu");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.TituloMenu).HasColumnName("titulomenu");
                entity.Property(e => e.EstadoMenu).HasColumnName("estadomenu");
            });
            modelBuilder.Entity<TiendaNetApi.Model.RecetaXMenu>(entity =>
            {
                entity.ToTable("recetasxmenu");
                entity.HasKey(e => new { e.RecetaId, e.MenuId });

                entity.Property(e => e.RecetaId).HasColumnName("recetaid");
                entity.Property(e => e.MenuId).HasColumnName("menuid");
            });

        }

    }
}