using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class DetalleobjetoConfiguration : IEntityTypeConfiguration<Detalleobjeto>
    {
        public void Configure(EntityTypeBuilder<Detalleobjeto> entity)
        {
            // Clave compartida: idObjeto es PK y FK a la vez (relación 1 a 1).
            entity.HasKey(e => e.IdObjeto).HasName("PRIMARY");

            entity.ToTable("detalleobjeto");

            entity.HasIndex(e => e.IdLugarAlmacenamiento, "idx_detalle_lugar");
            entity.HasIndex(e => e.IdUsuarioRegistro, "idx_detalle_usuario");

            entity.Property(e => e.IdObjeto)
                .ValueGeneratedNever()
                .HasColumnName("idObjeto");
            entity.Property(e => e.LugarEncontrado).HasColumnName("lugarEncontrado").HasMaxLength(100);
            entity.Property(e => e.FechaEncontrada).HasColumnName("fechaEncontrada");
            entity.Property(e => e.FechaEntrega).HasColumnName("fechaEntrega");
            entity.Property(e => e.DetalleVerificacion).HasColumnName("detalleVerificacion").HasColumnType("text");
            entity.Property(e => e.IdLugarAlmacenamiento).HasColumnName("idLugarAlmacenamiento");
            entity.Property(e => e.IdUsuarioRegistro).HasColumnName("idUsuarioRegistro");

            // Si se borra el objeto, se borra su detalle.
            entity.HasOne(d => d.IdObjetoNavigation).WithOne(p => p.Detalle)
                .HasForeignKey<Detalleobjeto>(d => d.IdObjeto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_detalle_objeto");

            entity.HasOne(d => d.IdLugarAlmacenamientoNavigation).WithMany(p => p.Detalleobjetos)
                .HasForeignKey(d => d.IdLugarAlmacenamiento)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detalle_lugar");

            entity.HasOne(d => d.IdUsuarioRegistroNavigation).WithMany(p => p.DetallesRegistrados)
                .HasForeignKey(d => d.IdUsuarioRegistro)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_detalle_usuario");
        }
    }
}
