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

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

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
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1BDAB4D432");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses).HasConstraintName("FK__Addresses__UserI__5BE2A6F2");
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD79783E56CBE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Carts).HasConstraintName("FK__Cart__UserID__4D94879B");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B2A2388F701");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasConstraintName("FK__CartItems__CartI__7B5B524B");

            entity.HasOne(d => d.Item).WithMany(p => p.CartItems).HasConstraintName("FK__CartItems__ItemI__7C4F7684");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK__Coupons__384AF1BA637E4A06");
        });

        modelBuilder.Entity<DeliveryMan>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Delivery__F1B1CD244BC9E96E");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.HasKey(e => e.MenuCatId).HasName("PK__MenuCate__AF3411AFFD11B1D4");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.MenuCategories).HasConstraintName("FK__MenuCateg__Resta__6B24EA82");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__MenuItem__727E83EB8A37C042");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MenuCat).WithMany(p => p.MenuItems).HasConstraintName("FK__MenuItem__MenuCa__73BA3083");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF550C5CB1");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders).HasConstraintName("FK__Orders__AddressI__1CBC4616");

            entity.HasOne(d => d.Driver).WithMany(p => p.Orders).HasConstraintName("FK__Orders__DriverID__1BC821DD");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders).HasConstraintName("FK__Orders__PaymentI__1DB06A4F");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders).HasConstraintName("FK__Orders__Restaura__19DFD96B");

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasConstraintName("FK__Orders__UserID__1AD3FDA4");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A1A1B7A395");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__ItemI__25518C17");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Order__245D67DE");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A38C19AB1EF");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__Restaura__87454CB58A12C5AE");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Address).WithMany(p => p.Restaurants).HasConstraintName("FK__Restauran__Addre__6477ECF3");

            entity.HasOne(d => d.Owner).WithMany(p => p.Restaurants).HasConstraintName("FK__Restauran__Owner__6383C8BA");
        });

        modelBuilder.Entity<RestaurantPhone>(entity =>
        {
            entity.HasKey(e => new { e.RestaurantId, e.PhoneNumber }).HasName("PK__Restaura__0F1AF856067F1DEB");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.RestaurantPhones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Restauran__Resta__339FAB6E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Role__1788CCAC2CDB680D");

            entity.Property(e => e.UserId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithOne(p => p.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Role__UserID__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC748C8984");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InActive).HasDefaultValue(false);

            entity.HasOne(d => d.IdentityUser).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_AspNetUsers");
        });

        modelBuilder.Entity<UserPhone>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PhoneNumber }).HasName("PK__User_Pho__9FD7784F25D9869C");

            entity.HasOne(d => d.User).WithMany(p => p.UserPhones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Phon__UserI__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
