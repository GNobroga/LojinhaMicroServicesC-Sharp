using Lojinha.Consumer.Web.Models;
using Lojinha.Consumer.Web.Utils;

namespace Lojinha.Consumer.Web.Services;

public class CouponService : ICouponService
{
    private readonly HttpClient _httpClient;

    public CouponService(IHttpClientFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _httpClient = factory.CreateClient("CouponAPI");
    }
    public async Task<Coupon> GetCoupon(string code)
    {
        var response = await _httpClient.MakeGetRequest(code);
        return await response.ConvertJsonToModel<Coupon>();
    }
}