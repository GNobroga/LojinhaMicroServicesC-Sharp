namespace Lojinha.Cart.API.Utils;

public class InvalidObjectResponse : BaseApplicationResponse
{
    public List<string> Messages { get; set; } = new();
}