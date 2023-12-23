using System.ComponentModel.DataAnnotations;

namespace Lojinha.Consumer.Web.Models;

public class CartDetail 
{
    public long Id { get; set; }
    public long ItemId { get; set; }
    
    public ItemModel? Item { get; set; }

    public string? CouponCode { get; set; } 

    public long Quantity { get; set; }
}