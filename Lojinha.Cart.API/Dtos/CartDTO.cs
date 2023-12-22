namespace Lojinha.Cart.API.Dtos;

public record CartDTO(
    long Id, 
    string UserId,
    bool Finished,
    ICollection<CartDetailDTO> CartDetails
);