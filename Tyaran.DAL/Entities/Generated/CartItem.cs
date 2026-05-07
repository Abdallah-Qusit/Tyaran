using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

public partial class CartItem
{
    [Key]
    [Column("CartItemID")]
    public int CartItemId { get; set; }

    [StringLength(255)]
    public string? SpecialInst { get; set; }

    public int? Quantity { get; set; }

    [Column("CartID")]
    public int? CartId { get; set; }

    [Column("ItemID")]
    public int? ItemId { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartItems")]
    public virtual Cart? Cart { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("CartItems")]
    public virtual MenuItem? Item { get; set; }
}
