using FluentValidation;

namespace Lojinha.Cart.API.Dtos.Validators;

public class CartDetailDTOValidator : AbstractValidator<CartDetailDTO>
{

    public CartDetailDTOValidator()
    {
        RuleFor(cd => cd.UserId)
            .NotEmpty().WithMessage("O 'userId' não pode ser vazio.")
            .NotNull().WithMessage("O 'userId' não pode ser null.");

        RuleFor(cd => cd.Count)
           .GreaterThan(0).WithMessage("O 'count' não pode ser menor ou igual a zero");   
    }
}