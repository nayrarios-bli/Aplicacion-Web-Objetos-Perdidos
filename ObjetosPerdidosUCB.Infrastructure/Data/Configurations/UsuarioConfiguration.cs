using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
            entity.Property(e => e.Contrasena).HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(150);
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(30);
        }
    }
};