using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tyaran.DAL.Entities.Generated;

[Table("MenuCategory")]
public partial class MenuCategory
{
    [Key]
    [Column("MenuCatID")]
    public int MenuCatId { get; set; }

    public bool? InActive { get; set; }

    [StringLength(100)]
    public string? Cartname { get; set; }

    [Column("RestaurantID")]
    public int? RestaurantId { get; set; }

    [InverseProperty("MenuCat")]
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    [ForeignKey("RestaurantId")]
    [InverseProperty("MenuCategories")]
    public virtual Restaurant? Restaurant { get; set; }
}
