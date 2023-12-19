namespace Lojinha.Cart.API.Utils;

public class InvalidObject
{
    public string Title { get; set; } = "Cart API Error";
    public List<string> Messages { get; set; } = new();

    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}