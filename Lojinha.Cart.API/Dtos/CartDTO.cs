namespace Lojinha.Cart.API.Dtos;

public record CartDTO(ICollection<CartDetailDTO> CartDetails);