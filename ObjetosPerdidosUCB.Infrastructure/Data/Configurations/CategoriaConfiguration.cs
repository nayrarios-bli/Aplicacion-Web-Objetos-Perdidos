using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.HasIndex(e => e.Nombre, "uq_categoria_nombre").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(150);
            entity.Property(e => e.Estado).HasColumnName("estado");
        }
    }
}
