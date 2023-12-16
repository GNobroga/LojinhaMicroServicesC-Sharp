using System.ComponentModel.DataAnnotations;

namespace Lojinha.Consumer.Web.Models;

public class ItemModel
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, 10000)]
    public decimal Price { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(300)]
    public string? ImageUrl { get; set; }
}