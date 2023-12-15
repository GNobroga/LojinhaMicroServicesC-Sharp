using System.ComponentModel.DataAnnotations;

namespace Lojinha.Product.API.Data.ValueObjects;
public class ItemVO
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "O {0} é obrigatório")]
    [StringLength(150, ErrorMessage = "O {0} só pode ter no máximo 150 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O {0} é obrigatório")]
    [Range(1, 10000, ErrorMessage = "O {0} deve estar entre 1 e 10000")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "A {0} só pode ter no máximo 500 caracteres")]
    public string? Description { get; set; }

    [StringLength(50,ErrorMessage = "A {0} só pode ter no máximo 50 caracteres")]
    [Required(ErrorMessage = "O {0} é obrigatório")]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "A {0} só pode ter no máximo 300 caracteres")]
    public string? ImageUrl { get; set; }
   
}