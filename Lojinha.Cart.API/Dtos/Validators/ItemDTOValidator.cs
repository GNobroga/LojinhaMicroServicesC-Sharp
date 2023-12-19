using FluentValidation;

namespace Lojinha.Cart.API.Dtos.Validators;

public class ItemDTOValidator : AbstractValidator<ItemDTO>
{
    public ItemDTOValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("O nome não pode ser vazio.")
            .NotNull().WithMessage("O nome não pode ser null.");
        
        RuleFor(i => i.Price)
            .LessThan(0).WithMessage("O preço não pode ser menor que zero.");
    }
}