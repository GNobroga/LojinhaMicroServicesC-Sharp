using FluentValidation;

namespace Lojinha.Cart.API.Dtos.Validators;

public class ItemDTOValidator : AbstractValidator<ItemDTO>
{
    public ItemDTOValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("The item name is null or empty");
        
        RuleFor(i => i.Price)
            .LessThan(0)
            .WithMessage("The price is less then or equals to zero");
    }
}