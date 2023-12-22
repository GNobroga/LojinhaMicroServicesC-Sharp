namespace Lojinha.Cart.API.Dtos;
public record CartDetailDTO(
    long Id,
    long ItemId,
    ItemDTO Item,
    string? CouponCode,
    long Quantity
);

