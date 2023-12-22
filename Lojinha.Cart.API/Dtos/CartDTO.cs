namespace Lojinha.Cart.API.Dtos;

public record CartDTO(
    long Id, 
    bool Finished,
    ICollection<CartDetailDTO> CartDetails
);