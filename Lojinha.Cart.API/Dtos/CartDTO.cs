namespace Lojinha.Cart.API.Dtos;

public record CartDTO(
    long Id, 
    bool Finished,
    string UserId,
    ICollection<CartDetailDTO> CartDetails
);