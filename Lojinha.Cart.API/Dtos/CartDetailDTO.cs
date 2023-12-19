using System.ComponentModel.DataAnnotations;

namespace Lojinha.Cart.API.Dtos;
public record CartDetailDTO(
    long Id,
    string UserId,
    string? CouponCode,
    long ItemId,
    ItemDTO? Item,
    long Count
);

