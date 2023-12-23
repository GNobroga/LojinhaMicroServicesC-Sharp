using Lojinha.Consumer.Web.Models;

namespace Lojinha.Consumer.Web.Services;

public interface ICouponService
{
    Task<Coupon> GetCoupon(string code);
}