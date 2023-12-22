using System.ComponentModel.DataAnnotations;

namespace Lojinha.Cart.API.Dtos;
public record CartDetailDTO(
    long Id,
    long ItemId,
    long Quantity,
    long CartId
);

