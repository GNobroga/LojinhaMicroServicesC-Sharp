using FluentValidation;

namespace Lojinha.Cart.API.Dtos.Validators;

public class CartDTOValidator : AbstractValidator<CartDTO>
{
    public CartDTOValidator()
    {
        RuleFor(c => c.CartDetails)
            .ForEach(cd => cd.SetValidator(new CartDetailDTOValidator()));
    }
}