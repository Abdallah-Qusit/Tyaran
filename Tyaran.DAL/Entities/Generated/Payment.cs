using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("Payment")]
public partial class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [StringLength(100)]
    public string? TransactionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PaidAt { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Amount { get; set; }

    [InverseProperty("Payment")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
