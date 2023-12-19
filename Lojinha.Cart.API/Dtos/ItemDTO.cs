namespace Lojinha.Cart.API.Dtos;

public record ItemDTO(
    long Id,
    string Name,
    decimal Price,
    string Description,
    string CategoryName,
    string ImageUrl
);