using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Entities
{
    public partial class Proj0Context : DbContext
    {
        public Proj0Context()
        {
        }

        public Proj0Context(DbContextOptions<Proj0Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Inventories> Inventories { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:liangtonyserver.database.windows.net,1433;Initial Catalog=Proj0;Persist Security Info=False;User ID=tony;Password=XstarfireX2684;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.PreferLocationId).HasColumnName("PreferLocationID");

                entity.Property(e => e.PreferProductId).HasColumnName("PreferProductID");

                entity.HasOne(d => d.PreferLocation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferLocationId)
                    .HasConstraintName("FK__Customers__Prefe__3B0BC30C");

                entity.HasOne(d => d.PreferProduct)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PreferProductId)
                    .HasConstraintName("FK__Customers__Prefe__3BFFE745");
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
                    .HasConstraintName("FK__Inventori__Produ__373B3228");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventori__Store__382F5661");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Order__43A1090D");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Produ__42ACE4D4");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DateTimeOrder).HasColumnType("datetime");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__Customer__3EDC53F0");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__StoreID__3FD07829");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.ProudctName)
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
