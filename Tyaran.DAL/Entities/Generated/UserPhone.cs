using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[PrimaryKey("UserId", "PhoneNumber")]
[Table("User_Phone")]
public partial class UserPhone
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Key]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserPhones")]
    public virtual User User { get; set; } = null!;
}
