namespace Lojinha.Order.API.Messages;
public record CartDetailDTO(
    long Id,
    long ItemId,
    ItemDTO Item,
    string? CouponCode,
    long Quantity
);

