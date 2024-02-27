using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace shopping_cart_backend.Models;

public partial class ShoppingCartDbContext : DbContext
{
    public ShoppingCartDbContext()
    {
    }

    public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<ProductsTbl> ProductsTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8BL3MIG\\SQLEXPRESS;Database=ShoppingCartDb;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3214EC2704F45A61");

            entity.ToTable("Cart");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<ProductsTbl>(entity =>
        {
            entity.ToTable("Products_tbl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActualPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductImage)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
