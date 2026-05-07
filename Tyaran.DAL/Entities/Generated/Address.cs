using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

public partial class Address
{
    [Key]
    [Column("AddressID")]
    public int AddressId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(50)]
    public string? Building { get; set; }

    [StringLength(20)]
    public string? Floor { get; set; }

    [StringLength(20)]
    public string? Apartment { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public bool? IsDefault { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Address")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Address")]
    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    [ForeignKey("UserId")]
    [InverseProperty("Addresses")]
    public virtual User? User { get; set; }
}
