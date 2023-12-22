namespace Lojinha.Consumer.Web.Models;

public class Cart
{

    public long Id { get; set; }
    public string? UserId { get; set; }

    public bool Finished { get; set; }

    public List<CartDetail> CartDetails { get; set; } = new();

    public decimal Total { get; set; }
}