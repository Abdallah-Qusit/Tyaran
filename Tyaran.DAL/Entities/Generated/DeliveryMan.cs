using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("DeliveryMan")]
public partial class DeliveryMan
{
    [Key]
    [Column("DriverID")]
    public int DriverId { get; set; }
    public int UserId { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [StringLength(50)]
    public string? VehiclePlate { get; set; }

    public bool? IsOnline { get; set; }

    public bool? IsAvailable { get; set; }

    public int ApprovalStatus { get; set; } = 0;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [StringLength(50)]
    public string? VehicleType { get; set; }

    [StringLength(255)]
    public string? NationalIdPath { get; set; }

    [InverseProperty("Driver")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("UserId")]
    [InverseProperty("DeliveryMan")]
    public virtual User User { get; set; } = null!;

}
