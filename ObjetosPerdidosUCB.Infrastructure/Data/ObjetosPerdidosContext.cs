using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Data;

public partial class ObjetosPerdidosContext : DbContext
{
    public ObjetosPerdidosContext(DbContextOptions<ObjetosPerdidosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Detalleobjeto> Detalleobjetos { get; set; }

    public virtual DbSet<LugarAlmacenamiento> LugaresAlmacenamiento { get; set; }

    public virtual DbSet<Objeto> Objetos { get; set; }

    public virtual DbSet<Reclamo> Reclamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        // Sin esta línea, las clases *Configuration NO se aplican.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
