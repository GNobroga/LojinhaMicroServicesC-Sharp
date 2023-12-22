using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Cart.API.Entities;

[Table("cart")]
public class Cart : BaseEntity
{
    [Column("user_id")]
    public string? UserId { get; set; }

    [Column("coupon_code")]
    public string? CouponCode { get; set; }

    [Column("finished")]
    public bool Finished { get; set; }

    public List<CartDetail> CartDetails { get; set; } = new();
}