using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Entities
{
    public partial class Proj1Context : DbContext
    {
        public Proj1Context()
        {
        }

        public Proj1Context(DbContextOptions<Proj1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Inventories> Inventories { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<RoleTypes> RoleTypes { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Customer__C9F284564A6311EE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.PreferLocationId).HasColumnName("PreferLocationID");

                entity.Property(e => e.PreferProductId).HasColumnName("PreferProductID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.PreferLocation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferLocationId)
                    .HasConstraintName("FK__Customers__Prefe__3D1EF407");

                entity.HasOne(d => d.PreferProduct)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferProductId)
                    .HasConstraintName("FK__Customers__Prefe__3E131840");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Employee__C9F284560FE6F601")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employees__RoleI__357DD23F");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employees__Store__3489AE06");
            });

            modelBuilder.Entity<Inventories>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventori__Produ__385A3EEA");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventori__Store__394E6323");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__45B43A08");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Produ__44C015CF");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Customer__40EF84EB");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__StoreID__41E3A924");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.ImagePath).HasMaxLength(256);

                entity.Property(e => e.ProudctName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<RoleTypes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Zip).HasColumnName("zip");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
