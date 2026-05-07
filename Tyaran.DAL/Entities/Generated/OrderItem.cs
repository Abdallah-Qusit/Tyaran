using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("OrderItem")]
public partial class OrderItem
{
    [Key]
    [Column("OrderItemID")]
    public int OrderItemId { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? UnitPrice { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalPrice { get; set; }

    [Column("ItemID")]
    public int? ItemId { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("OrderItems")]
    public virtual MenuItem? Item { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order? Order { get; set; }
}
