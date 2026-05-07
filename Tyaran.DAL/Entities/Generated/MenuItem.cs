using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("MenuItem")]
public partial class MenuItem
{
    [Key]
    [Column("ItemID")]
    public int ItemId { get; set; }

    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    public int? PreparationTime { get; set; }

    public bool? IsAvailable { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column("MenuCatID")]
    public int? MenuCatId { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("MenuCatId")]
    [InverseProperty("MenuItems")]
    public virtual MenuCategory? MenuCat { get; set; }

    [InverseProperty("Item")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
