using Microsoft.AspNetCore.Components.Web;

namespace Lojinha.Product.API.Utils;

public class Error
{

    public string Title { get; set; } = "API Error";

    public int StatusCode { get; set; } = 400;

    public string Message { get; set; } = string.Empty;

    public long Moment { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    public Error() {}
    public Error(string message)
    {
        Message = message;
    }

    public Error(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}