namespace Lojinha.Coupon.API.ObjectValues;

public class CouponVO
{
    public long Id { get; set; }

    public string? CouponCode { get; set; }

    public decimal Discount { get; set; }
}