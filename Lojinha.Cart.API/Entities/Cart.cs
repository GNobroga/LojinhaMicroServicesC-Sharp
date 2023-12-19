namespace Lojinha.Cart.API.Entities;

public class Cart 
{
    public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
}