using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Coupon.API.Entities;

public class Coupon 
{   
    [Column("id")]
    public long CouponId { get; set; }

    [Column("coupon_code")]
    [StringLength(100, ErrorMessage = "The max length is {1}")]
    [Required(ErrorMessage = "The coupon code is required")]
    public string? CouponCode { get; set; }

    [Column("discount")]
    public decimal Discount { get; set; }
}