
using FluentValidation;
using Lojinha.Cart.API.Utils;

namespace Lojinha.Cart.API.Dtos.Filters;

public class DTOValidatorFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public DTOValidatorFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var dto = context.Arguments.FirstOrDefault(dto => dto?.GetType() == typeof(T)) as T;

        var result = await _validator.ValidateAsync(dto!);

        if (!result.IsValid)
        {
            var invalidObject = new InvalidObject();

            result.Errors.ForEach(error => invalidObject.Messages.Add(error.ErrorMessage));

            return Results.Json(invalidObject, statusCode: 400);
        }

        return await next(context);
    }
}