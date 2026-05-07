using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [StringLength(50)]
    public string? OrderStatus { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Subtotal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeliveredAt { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? TotalAmount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? DeliveryFee { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Tax { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Discount { get; set; }

    [Column("RestaurantID")]
    public int? RestaurantId { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("DriverID")]
    public int? DriverId { get; set; }

    [Column("AddressID")]
    public int? AddressId { get; set; }

    [Column("PaymentID")]
    public int? PaymentId { get; set; }

    [ForeignKey("AddressId")]
    [InverseProperty("Orders")]
    public virtual Address? Address { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("Orders")]
    public virtual DeliveryMan? Driver { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("PaymentId")]
    [InverseProperty("Orders")]
    public virtual Payment? Payment { get; set; }

    [ForeignKey("RestaurantId")]
    [InverseProperty("Orders")]
    public virtual Restaurant? Restaurant { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User? User { get; set; }
}
