using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class ReclamoConfiguration : IEntityTypeConfiguration<Reclamo>
    {
        public void Configure(EntityTypeBuilder<Reclamo> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reclamo");

            entity.HasIndex(e => new { e.IdObjeto, e.Estado }, "idx_reclamo_objeto_estado");
            entity.HasIndex(e => e.IdUsuario, "idx_reclamo_usuario");
            entity.HasIndex(e => e.IdRevisor, "idx_reclamo_revisor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Justificacion).HasColumnName("justificacion").HasColumnType("text");
            entity.Property(e => e.FechaEmitida).HasColumnName("fechaEmitida").HasColumnType("datetime");
            entity.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(20);
            entity.Property(e => e.FechaRevisada).HasColumnName("fechaRevisada").HasColumnType("datetime");
            entity.Property(e => e.Observacion).HasColumnName("observacion").HasMaxLength(255);
            entity.Property(e => e.IdObjeto).HasColumnName("idObjeto");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.IdRevisor).HasColumnName("idRevisor");

            entity.HasOne(d => d.IdObjetoNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdObjeto)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_reclamo_objeto");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_reclamo_usuario");

            entity.HasOne(d => d.IdRevisorNavigation).WithMany(p => p.ReclamosRevisados)
                .HasForeignKey(d => d.IdRevisor)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_reclamo_revisor");
        }
    }
}
