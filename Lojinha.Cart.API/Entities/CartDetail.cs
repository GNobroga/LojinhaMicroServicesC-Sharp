using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Cart.API.Entities;

[Table("cart_details")]
public class CartDetail : BaseEntity
{   

    [Column("item_id")]
    [ForeignKey(nameof(Item))]
    public long ItemId { get; set; }

    public Item? Item { get; set; }

    [Column("quantity")]
    public long Quantity { get; set; }

    [Column("cart_id")]
    public long CartId { get; set; }

    [ForeignKey("CartId")]
    public Cart? Cart { get; set; }

    public void PlusQuantity(long quantity)
    {
        Quantity += quantity;
    }

    public void MinusQuantity(long quantity)
    {
        if (Quantity > 0 && Quantity >= quantity)
            Quantity -= quantity;
    }
}