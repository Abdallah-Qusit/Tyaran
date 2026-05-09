using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("Restaurant")]
public partial class Restaurant
{
    [Key]
    [Column("RestaurantID")]
    public int RestaurantId { get; set; }

    [Column("OwnerID")]
    public int? OwnerId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Column("AddressID")]
    public int? AddressId { get; set; }

    [StringLength(255)]
    public string? LogoUrl { get; set; }

    [StringLength(255)]
    public string? CoverImageUrl { get; set; }

    public TimeOnly? OpeningTime { get; set; }

    public TimeOnly? ClosingTime { get; set; }

    public double? Rating { get; set; }

    public bool? IsActive { get; set; }
    public int ApprovalStatus { get; set; } = 0;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? DeliveryFee { get; set; }

    [StringLength(255)]
    public string? Review { get; set; }

    [StringLength(255)]
    public string? CommercialRegisterPath { get; set; }

    [StringLength(255)]
    public string? LogoPath { get; set; }

    [StringLength(255)]
    public string? MenuItemPath { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Restaurants")]
    public virtual Address? Address { get; set; }

    [InverseProperty("Restaurant")]
    public virtual ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();

    [InverseProperty("Restaurant")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("OwnerId")]
    [InverseProperty("Restaurants")]
    public virtual User? Owner { get; set; }

    [InverseProperty("Restaurant")]
    public virtual ICollection<RestaurantPhone> RestaurantPhones { get; set; } = new List<RestaurantPhone>();
}
