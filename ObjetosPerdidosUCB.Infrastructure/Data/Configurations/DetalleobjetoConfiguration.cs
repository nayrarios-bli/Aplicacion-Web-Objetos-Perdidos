using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class DetalleobjetoConfiguration : IEntityTypeConfiguration<Detalleobjeto>
    {
        public void Configure(EntityTypeBuilder<Detalleobjeto> entity)
        {
         
           
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("detalleobjeto");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.Property(e => e.DiasGuardados).HasColumnName("diasGuardados");
                entity.Property(e => e.FechaEncontrada).HasColumnName("fechaEncontrada");
                entity.Property(e => e.FechaRecogida).HasColumnName("fechaRecogida");
                entity.Property(e => e.LugarAlmacenado)
                    .HasMaxLength(75)
                    .HasColumnName("lugarAlmacenado");
                entity.Property(e => e.LugarEncontrado)
                    .HasMaxLength(70)
                    .HasColumnName("lugarEncontrado");

              
            
        }
    }
}
