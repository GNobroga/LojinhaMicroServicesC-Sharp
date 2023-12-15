using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lojinha.Product.API.Models.Base;

namespace Lojinha.Product.API.Models;

[Table("items")]
public class Item : BaseEntity
{   
    [Column("name")]
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    [Required]
    [Range(1, 10000)]
    public decimal Price { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("category_name")]
    [StringLength(50)]
    public string CategoryName { get; set; } = string.Empty;

    [Column("image_url")]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
}