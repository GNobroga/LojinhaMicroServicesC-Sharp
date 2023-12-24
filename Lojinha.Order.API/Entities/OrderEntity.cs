using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Order.API.Entities;

[Table("orders")]
public class OrderEntity
{
    public long Id { get; set; }

    [Required(ErrorMessage = "The userId is required for the best result")]
    public string? UserId { get; set; }

    public bool Finished { get; set; }

    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [Column("total")]
    public decimal Total { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("purchase_date")]
    public DateTime DateTime { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("email")]
    public string? Email { get; set; }

    [Column("card_number")]
    public string? CardNumber { get; set; }

    [Column("cvv")]
    public string? CVV { get; set; }

    [Column("expiry_month_year")]
    public string? ExpiryMothYear { get; set; }

    [Column("status")]
    public bool Status { get; set; }

}