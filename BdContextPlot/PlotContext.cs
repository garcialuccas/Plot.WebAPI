using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Plot.WebAPI.Models;

namespace Plot.WebAPI.BdContextPlot;

public partial class PlotContext : DbContext
{
    public PlotContext()
    {
    }

    public PlotContext(DbContextOptions<PlotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Filme> Filme { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D24S20-1251883\\MSSQLSERVER2;Database=PlotDB;User Id=sa;Password=Senai@134;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filme>(entity =>
        {
            entity.HasKey(e => e.IdFilme).HasName("PK__Filme__6E8F2A7654540EF8");

            entity.Property(e => e.IdFilme).HasDefaultValueSql("(newid())");

            entity.HasMany(d => d.IdGenero).WithMany(p => p.IdFilme)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmeGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("IdGenero")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GeneroFilmeGenero"),
                    l => l.HasOne<Filme>().WithMany()
                        .HasForeignKey("IdFilme")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_FilmeFilmeGenero"),
                    j =>
                    {
                        j.HasKey("IdFilme", "IdGenero");
                    });
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F834988E7421E29");

            entity.Property(e => e.IdGenero).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97BDC7C3CB");

            entity.ToTable(tb => tb.HasTrigger("TR_UsuarioSoftDelete"));

            entity.Property(e => e.IdUsuario).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Ativo).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
