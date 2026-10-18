using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    internal class ReclamoConfiguration :
        IEntityTypeConfiguration<Reclamo>
    {
        public void Configure(EntityTypeBuilder<Reclamo> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reclamo");

            entity.HasIndex(e => e.IdObjeto, "idx_reclamo_objeto");

            entity.HasIndex(e => e.IdUsuario, "idx_reclamo_usuario");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pendiente'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaEmitida)
                .HasColumnType("datetime")
                .HasColumnName("fechaEmitida");
            entity.Property(e => e.FechaRevisada)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("fechaRevisada");
            entity.Property(e => e.IdObjeto)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idObjeto");
            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idUsuario");

            entity.HasOne(d => d.IdObjetoNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdObjeto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reclamo_objeto");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reclamos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reclamo_usuario");
        }
    }
}
