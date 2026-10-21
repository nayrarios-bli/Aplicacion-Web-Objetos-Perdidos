using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class ObjetoConfiguration : IEntityTypeConfiguration<Objeto>
    {
        public void Configure(EntityTypeBuilder<Objeto> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("objeto");

            entity.HasIndex(e => e.Codigo, "uq_objeto_codigo").IsUnique();
            entity.HasIndex(e => e.IdCategoria, "idx_objeto_categoria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo).HasColumnName("codigo").HasMaxLength(30);
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasColumnType("text");
            entity.Property(e => e.Imagen).HasColumnName("imagen").HasMaxLength(255);
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            // Estado sin HasDefaultValueSql: con un bool, EF trataría "false" como
            // "sin valor" y no lo enviaría al INSERT (el default de la BD lo volvería true).

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Objetos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_objeto_categoria");
        }
    }
}
