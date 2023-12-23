namespace Lojinha.Consumer.Web.Models;

public class Coupon 
{
    public long Id { get; set; }

    public string? CouponCode { get; set; }

    public decimal Discount { get; set; }
}