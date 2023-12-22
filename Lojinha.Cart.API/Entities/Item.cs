using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Cart.API.Entities;

[Table("items")]
public class Item : BaseEntity
{   
    [Column("name")]
    public string? Name { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("category_name")]
    public string? CategoryName { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [Column("count")]
    public int Count { get; set; } = 1;
}