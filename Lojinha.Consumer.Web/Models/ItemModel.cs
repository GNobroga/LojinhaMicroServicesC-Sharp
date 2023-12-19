using System.ComponentModel.DataAnnotations;

namespace Lojinha.Consumer.Web.Models;

public class ItemModel
{   
    [Key]
    public long Id { get; set; }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "O {0} é um campo requerido.")]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Preço")]
    [Required(ErrorMessage = "O {0} é um campo requerido.")]
    [Range(1, 10000, ErrorMessage = "O {0} precisa estar entre {1} e {2}")]
    public decimal Price { get; set; }

    [Display(Name = "Descrição")]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Categoria")]
    [Required(ErrorMessage = "O {0} é um campo requerido.")]
    [StringLength(50)]
    public string CategoryName { get; set; } = string.Empty;

    [Display(Name = "Imagem")]
    [StringLength(300)]
    public string ImageUrl { get; set; } = string.Empty;

    [Range(1, 100, ErrorMessage = "A quantidade deve estar entre {1} e {2}")]
    public int Count { get; set; } = 1;

    public string SubstringName(int limit)
    {
        return Name.Length < limit ? Name : Name[limit..];
    }

      public string SubstringDescription(int limit)
    {
        return Description.Length < limit ? Description : Description[limit..];
    }
}