using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Database;

public partial class TyaranDbContext : DbContext
{
    public TyaranDbContext(DbContextOptions<TyaranDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<DeliveryMan> DeliveryMen { get; set; }

    public virtual DbSet<MenuCategory> MenuCategories { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<RestaurantPhone> RestaurantPhones { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPhone> UserPhones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1B6656545E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses).HasConstraintName("FK__Addresses__UserI__1332DBDC");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD797E6F8F5A6");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Carts).HasConstraintName("FK__Cart__UserID__03F0984C");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B2A674462E7");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasConstraintName("FK__CartItems__CartI__40F9A68C");

            entity.HasOne(d => d.Item).WithMany(p => p.CartItems).HasConstraintName("FK__CartItems__ItemI__41EDCAC5");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK__Coupons__384AF1BABC3C0413");
        });

        modelBuilder.Entity<DeliveryMan>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Delivery__F1B1CD243CA4865A");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.HasKey(e => e.MenuCatId).HasName("PK__MenuCate__AF3411AF5BFFCE4C");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.MenuCategories).HasConstraintName("FK__MenuCateg__Resta__30C33EC3");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__MenuItem__727E83EB3947BE51");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MenuCat).WithMany(p => p.MenuItems).HasConstraintName("FK__MenuItem__MenuCa__395884C4");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF120E3954");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders).HasConstraintName("FK__Orders__AddressI__531856C7");

            entity.HasOne(d => d.Driver).WithMany(p => p.Orders).HasConstraintName("FK__Orders__DriverID__5224328E");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders).HasConstraintName("FK__Orders__PaymentI__540C7B00");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders).HasConstraintName("FK__Orders__Restaura__503BEA1C");

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasConstraintName("FK__Orders__UserID__51300E55");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A1353122E4");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__ItemI__5BAD9CC8");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Order__5AB9788F");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38BDB1F751");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__Restaura__87454CB51C0C2390");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Address).WithMany(p => p.Restaurants).HasConstraintName("FK__Restauran__Addre__1BC821DD");

            entity.HasOne(d => d.Owner).WithMany(p => p.Restaurants).HasConstraintName("FK__Restauran__Owner__1AD3FDA4");
        });

        modelBuilder.Entity<RestaurantPhone>(entity =>
        {
            entity.HasKey(e => new { e.RestaurantId, e.PhoneNumber }).HasName("PK__Restaura__0F1AF8565B93703F");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.RestaurantPhones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Restauran__Resta__6166761E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Role__1788CCAC88AF1719");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithOne(p => p.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Role__UserID__72C60C4A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC1DE3C551");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InActive).HasDefaultValue(false);
        });

        modelBuilder.Entity<UserPhone>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PhoneNumber }).HasName("PK__User_Pho__9FD7784FE7C18143");

            entity.HasOne(d => d.User).WithMany(p => p.UserPhones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Phon__UserI__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
