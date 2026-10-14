using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Entities;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ObjetosPerdidosUCB.Api.Data;

public partial class ObjetosPerdidosContext : DbContext
{
    public ObjetosPerdidosContext()
    {
    }

    public ObjetosPerdidosContext(DbContextOptions<ObjetosPerdidosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Objeto> Objetos { get; set; }

    public virtual DbSet<Reclamo> Reclamos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=dbObjetosPerdidosUCB;uid=root;pwd=P@ssw0rd", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.46-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Objeto>(entity =>
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
        });

        modelBuilder.Entity<Usuario>(entity =>
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
