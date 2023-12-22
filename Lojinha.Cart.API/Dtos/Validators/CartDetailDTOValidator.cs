using FluentValidation;

namespace Lojinha.Cart.API.Dtos.Validators;

public class CartDetailDTOValidator : AbstractValidator<CartDetailDTO>
{

    public CartDetailDTOValidator()
    {
        RuleFor(cd => cd.CartId)
            .NotEmpty()
            .NotNull()
            .WithMessage("The cardId is empty or null");

        RuleFor(cd => cd.ItemId)
            .NotEmpty()
            .NotNull()
            .WithMessage("The itemId is empty or null");

        RuleFor(cd => cd.Quantity)
           .GreaterThan(0)
           .WithMessage("The quantity needs be greather than or equal to zero");   
    }
}