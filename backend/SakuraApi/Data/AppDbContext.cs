using Microsoft.EntityFrameworkCore;
using SakuraApi.Models;

namespace SakuraApi.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<Novedad> Novedades => Set<Novedad>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("Categories");
            entity.Property(categoria => categoria.Nombre).HasColumnName("Name");
            entity.Property(categoria => categoria.Descripcion).HasColumnName("Description");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Products");
            entity.Property(producto => producto.Nombre).HasColumnName("Name");
            entity.Property(producto => producto.Descripcion).HasColumnName("Description");
            entity.Property(producto => producto.Precio).HasColumnName("Price").HasPrecision(10, 2);
            entity.Property(producto => producto.CategoriaId).HasColumnName("CategoryId");
            entity.Property(producto => producto.ImagenUrl).HasColumnName("ImageUrl");
            entity.Property(producto => producto.EsDestacado).HasColumnName("IsFeatured");
            entity.Property(producto => producto.EstaDisponible).HasColumnName("IsAvailable");
            entity.HasOne(producto => producto.Categoria)
                .WithMany(categoria => categoria.Productos)
                .HasForeignKey(producto => producto.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.ToTable("Events");
            entity.Property(evento => evento.Titulo).HasColumnName("Title");
            entity.Property(evento => evento.Descripcion).HasColumnName("Description");
            entity.Property(evento => evento.IniciaEn).HasColumnName("StartsAt");
            entity.Property(evento => evento.Ubicacion).HasColumnName("Location");
            entity.Property(evento => evento.AficheUrl).HasColumnName("PosterUrl");
            entity.Property(evento => evento.Estado)
                .HasColumnName("Status")
                .HasConversion(
                    estado => ConvertirEstadoEventoATexto(estado),
                    estado => ConvertirTextoAEstadoEvento(estado));
        });

        modelBuilder.Entity<Novedad>(entity =>
        {
            entity.ToTable("NewsPosts");
            entity.Property(novedad => novedad.Titulo).HasColumnName("Title");
            entity.Property(novedad => novedad.Resumen).HasColumnName("Summary");
            entity.Property(novedad => novedad.Contenido).HasColumnName("Body");
            entity.Property(novedad => novedad.ImagenUrl).HasColumnName("ImageUrl");
            entity.Property(novedad => novedad.EstaPublicada).HasColumnName("IsPublished");
            entity.Property(novedad => novedad.CreadaEn).HasColumnName("CreatedAt");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Users");
            entity.Property(usuario => usuario.Nombre).HasColumnName("Name");
            entity.Property(usuario => usuario.Correo).HasColumnName("Email");
            entity.Property(usuario => usuario.Rol).HasColumnName("Role");
            entity.Property(usuario => usuario.Estado).HasColumnName("IsActive");
            entity.Property(usuario => usuario.FechaRegistro).HasColumnName("CreatedAt");
        });
    }

    private static string ConvertirEstadoEventoATexto(EstadoEvento estado) =>
        estado switch
        {
            EstadoEvento.Publicado => "Published",
            EstadoEvento.Cancelado => "Cancelled",
            _ => throw new InvalidOperationException("Estado de evento no reconocido.")
        };

    private static EstadoEvento ConvertirTextoAEstadoEvento(string estado) =>
        estado switch
        {
            "Published" => EstadoEvento.Publicado,
            "Cancelled" => EstadoEvento.Cancelado,
            _ => throw new InvalidOperationException("Estado de evento almacenado no reconocido.")
        };
}
