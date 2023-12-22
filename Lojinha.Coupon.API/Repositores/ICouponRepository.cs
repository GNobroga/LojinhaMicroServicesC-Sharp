using Lojinha.Coupon.API.ObjectValues;

namespace Lojinha.Coupon.API.Repositores;

public interface ICouponRepository
{
    Task<CouponVO> GetByCode(string code);
}