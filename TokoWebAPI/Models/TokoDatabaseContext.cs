using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TokoWebAPI.Models;

public partial class TokoDatabaseContext : DbContext
{
    public TokoDatabaseContext()
    {
    }

    public TokoDatabaseContext(DbContextOptions<TokoDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__19093A2B54938562");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__B40CC6EDFB6AF65A");

            entity.ToTable("products");

            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProductID");
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Cat).WithMany(p => p.Products)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Categories_Products");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
