using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Cart.API.Entities;

// Items Cupom
[Table("cart_details")]
public class CartDetail : Base
{   
    [Column("user_id")]
    public string? UserId { get; set; }

    [Column("coupon_code")]
    public string? CouponCode { get; set; }

    [Column("item_id")]
    [ForeignKey(nameof(Item))]
    public long ItemId { get; set; }

    public Item? Item { get; set; }

    [Column("count")]
    public long Count { get; set; }

    [Column("paid")]
    public bool Paid { get; set; }

}