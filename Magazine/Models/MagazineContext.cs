using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Magazine.Models
{
    public partial class MagazineContext : DbContext
    {
        public MagazineContext()
        {
        }

        public MagazineContext(DbContextOptions<MagazineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ContactDetail> ContactDetails { get; set; } = null!;
        public virtual DbSet<Magazine> Magazines { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<OrderType> OrderTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Uom> Uoms { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=YourDbContextName");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DetailsId).HasColumnName("details_id");

                entity.HasOne(d => d.Details)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.DetailsId)
                    .HasConstraintName("FK__Client__details___39AD8A7F");
            });

            modelBuilder.Entity<ContactDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone");

                entity.Property(e => e.SecondName)
                    .HasMaxLength(255)
                    .HasColumnName("second_name");
            });

            modelBuilder.Entity<Magazine>(entity =>
            {
                entity.ToTable("Magazine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.TotalValue)
                    .HasColumnType("money")
                    .HasColumnName("total_value");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Order__client_id__46136164");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Order__status_id__4707859D");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Order__type_id__47FBA9D6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__user_id__451F3D2B");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__5090EFD7");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderItem__produ__4F9CCB9E");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<OrderType>(entity =>
            {
                entity.ToTable("OrderType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BaseUnit)
                    .HasMaxLength(50)
                    .HasColumnName("base_unit");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UomId).HasColumnName("uom_id");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.UomId)
                    .HasConstraintName("FK__Product__uom_id__4CC05EF3");
            });

            modelBuilder.Entity<Uom>(entity =>
            {
                entity.ToTable("UOM");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DetailsId).HasColumnName("details_id");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.HasOne(d => d.Details)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DetailsId)
                    .HasConstraintName("FK__User__details_id__3C89F72A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
