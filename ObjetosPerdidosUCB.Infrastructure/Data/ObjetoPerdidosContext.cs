using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using System.Reflection;

namespace ObjetosPerdidosUCB.Infrastructure.Data;

public partial class ObjetoPerdidosContext : DbContext
{
    public ObjetoPerdidosContext()
    {
    }

    public ObjetoPerdidosContext(DbContextOptions<ObjetoPerdidosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Detalleobjeto> Detalleobjetos { get; set; }

    public virtual DbSet<Objeto> Objetos { get; set; }

    public virtual DbSet<Reclamo> Reclamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=localhost;port=3306;database=dbobjetosperdidosucb;uid=root;pwd=P@ssw0rd", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
