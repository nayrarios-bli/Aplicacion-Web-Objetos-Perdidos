using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ObjetosPerdidosUCB.Core.Entities;

namespace ObjetosPerdidosUCB.Infrastructure.Data.Configurations
{
    public class LugarAlmacenamientoConfiguration : IEntityTypeConfiguration<LugarAlmacenamiento>
    {
        public void Configure(EntityTypeBuilder<LugarAlmacenamiento> entity)
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lugar_almacenamiento");

            entity.HasIndex(e => e.Nombre, "uq_lugar_nombre").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(75);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion").HasMaxLength(150);
            entity.Property(e => e.Estado).HasColumnName("estado");
        }
    }
}
