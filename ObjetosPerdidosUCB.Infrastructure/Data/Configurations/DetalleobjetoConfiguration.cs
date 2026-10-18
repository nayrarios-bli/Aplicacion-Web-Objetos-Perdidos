using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    internal class DetalleobjetoConfiguration :
        IEntityTypeConfiguration<Detalleobjeto>
    {
        public void Configure(EntityTypeBuilder<Detalleobjeto> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detalleobjeto");

            entity.HasIndex(e => e.Idobjeto, "fk_detalleObjeto_objeto1_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.DiasGuardados)
                .HasColumnType("tinyint(4)")
                .HasColumnName("diasGuardados");
            entity.Property(e => e.FechaEncontrada).HasColumnName("fechaEncontrada");
            entity.Property(e => e.FechaRecogida).HasColumnName("fechaRecogida");
            entity.Property(e => e.Idobjeto)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("idobjeto");
            entity.Property(e => e.LugarAlmacenado)
                .HasMaxLength(75)
                .HasColumnName("lugarAlmacenado");
            entity.Property(e => e.LugarEncontrado)
                .HasMaxLength(70)
                .HasColumnName("lugarEncontrado");

            entity.HasOne(d => d.IdobjetoNavigation).WithMany(p => p.Detalleobjetos)
                .HasForeignKey(d => d.Idobjeto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalleObjeto_objeto1");
        }
    }
}
