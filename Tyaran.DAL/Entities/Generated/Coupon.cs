using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Index("Code", Name = "UQ__Coupons__A25C5AA74C4D887B", IsUnique = true)]
public partial class Coupon
{
    [Key]
    public int CouponId { get; set; }

    [StringLength(50)]
    public string? DiscountType { get; set; }

    [StringLength(50)]
    public string? Code { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? MinimumOrder { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? DiscountValue { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }
}
