namespace Lojinha.Order.API.Messages;

public record ItemDTO(
    long Id,
    string Name,
    decimal Price,
    string Description,
    string CategoryName,
    string ImageUrl,
    int Count
);