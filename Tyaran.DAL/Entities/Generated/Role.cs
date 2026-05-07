using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("Role")]
public partial class Role
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Column("role")]
    [StringLength(50)]
    public string? Role1 { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Role")]
    public virtual User User { get; set; } = null!;
}
