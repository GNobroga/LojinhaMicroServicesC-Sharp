
using Microsoft.AspNetCore.Authentication;

namespace Lojinha.Consumer.Web.Interceptors;

public class RequestHandler : DelegatingHandler
{

    private readonly IHttpContextAccessor _accessor;

    public RequestHandler(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {   
    
        var token = await _accessor.HttpContext!.GetTokenAsync("access_token");

        if (token != null)
            request.Headers.Add("Authorization", $"Bearer {token}");

        return base.Send(request, cancellationToken);
    }
}