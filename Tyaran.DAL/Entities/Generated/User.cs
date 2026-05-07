using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("User")]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    public bool? InActive { get; set; }

    public int? CouponId { get; set; }

    [StringLength(450)]
    public string IdentityUserId { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("User")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("IdentityUserId")]
    [InverseProperty("Users")]
    public virtual AspNetUser IdentityUser { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Owner")]
    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    [InverseProperty("User")]
    public virtual Role? Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserPhone> UserPhones { get; set; } = new List<UserPhone>();
}
