namespace Lojinha.Cart.API.Dtos;
public record CartDetailDTO(
    long Id,
    long ItemId,
    string? CouponCode,
    long Quantity,
    long CartId
);

