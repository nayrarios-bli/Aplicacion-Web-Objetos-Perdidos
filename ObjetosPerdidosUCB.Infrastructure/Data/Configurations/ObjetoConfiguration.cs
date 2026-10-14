using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class ObjetoConfiguration : IEntityTypeConfiguration<Objeto>
    {
        public void Configure(EntityTypeBuilder<Objeto> entity)
        {
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("objeto");

                entity.HasIndex(e => e.IdUsuario, "idx_objeto_usuario");

                entity.HasIndex(e => e.Codigo, "uq_objeto_codigo").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Codigo).HasMaxLength(30);
                entity.Property(e => e.Descripcion).HasColumnType("text");
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("'1'");
                entity.Property(e => e.FechaEncontrada).HasColumnName("fechaEncontrada");
                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
                entity.Property(e => e.Imagen).HasMaxLength(255);
                entity.Property(e => e.LugarEncontrado)
                    .HasMaxLength(150)
                    .HasColumnName("lugarEncontrado");
                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Objetos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_objeto_usuario");
            }
        }
    }
}
