using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Order.API.Entities;

[Table("order_details")]
public class OrderDetail
{
    public long Id { get; set; }

    [Column("item_id")]
    public long ItemId { get; set; }

    [Column("product_name")]
    public string? ProductName { get; set; }
    public decimal Price { get; set; }

    [Column("coupon_code")]
    public string? CouponCode { get; set; }
    public long Quantity { get; set; }
}