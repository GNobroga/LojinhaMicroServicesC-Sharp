namespace Lojinha.Cart.API.Dtos;

public record CartDTO(
    long Id, 
    string UserId,
    string? CouponCode,
    bool Finished,
    ICollection<CartDetailDTO> CartDetails
);