using System.Data;
using FluentValidation;

namespace Lojinha.Cart.API.Dtos.Validators;

public class CartDTOValidator : AbstractValidator<CartDTO>
{
    public CartDTOValidator()
    {
        RuleFor(c => c.CartDetails)
            .ForEach(cd => cd.SetValidator(new CartDetailDTOValidator()));
        
        RuleFor(c => c.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("The userId cannot be null or empty.");
    }
}