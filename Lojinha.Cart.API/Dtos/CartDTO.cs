namespace Lojinha.Cart.API.Dtos;

public record CartDTO(long Id, ICollection<CartDetailDTO> CartDetails);
