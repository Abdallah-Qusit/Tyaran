using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[PrimaryKey("RestaurantId", "PhoneNumber")]
[Table("Restaurant_Phone")]
public partial class RestaurantPhone
{
    [Key]
    [Column("RestaurantID")]
    public int RestaurantId { get; set; }

    [Key]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [ForeignKey("RestaurantId")]
    [InverseProperty("RestaurantPhones")]
    public virtual Restaurant Restaurant { get; set; } = null!;
}
