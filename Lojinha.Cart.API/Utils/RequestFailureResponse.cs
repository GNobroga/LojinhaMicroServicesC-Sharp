namespace Lojinha.Cart.API.Utils;

public class RequestFailureResponse : BaseApplicationResponse
{
    public string Message { get; set; } = "Unknown error";

}