using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public partial class StockMarketContext : DbContext
    {
        public StockMarketContext(DbContextOptions<StockMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PRIMARY");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderID");
                entity.Property(e => e.StockId).HasColumnName("StockID");

                entity.HasOne(d => d.Stock).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StockId)
                    .HasConstraintName("orders_ibfk_1");

                // Use [JsonIgnore] attribute to prevent circular reference serialization
                entity.Navigation(e => e.Stock).UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.StockId).HasName("PRIMARY");

                entity.ToTable("Stock");

                entity.Property(e => e.StockId)
                    .ValueGeneratedNever()
                    .HasColumnName("StockID");
                entity.Property(e => e.StockName)
                    .HasMaxLength(255)
                    .HasColumnName("stockName");

                // Use [JsonIgnore] attribute to prevent circular reference serialization
                entity.Navigation(e => e.Orders).UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.NatId).HasName("PRIMARY");

                entity.ToTable("User");

                entity.HasIndex(e => e.UserName, "idx_user_name");

                entity.Property(e => e.NatId)
                    .HasMaxLength(14)
                    .HasColumnName("NatID");
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.UserName).HasColumnName("userName");
                entity.Property(e => e.Password).HasMaxLength(255).HasColumnName("userPassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
