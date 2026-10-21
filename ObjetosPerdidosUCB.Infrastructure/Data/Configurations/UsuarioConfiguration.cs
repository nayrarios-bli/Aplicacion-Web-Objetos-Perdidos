using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.CorreoElectronico, "uq_usuario_correo").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
            entity.Property(e => e.Contrasena).HasColumnName("contrasena").HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasColumnName("correoElectronico").HasMaxLength(150);
            entity.Property(e => e.Rol).HasColumnName("rol").HasMaxLength(30);
            entity.Property(e => e.Estado).HasColumnName("estado");
            
        }
    }
}
