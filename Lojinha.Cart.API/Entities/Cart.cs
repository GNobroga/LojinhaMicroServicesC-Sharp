using System.ComponentModel.DataAnnotations.Schema;

namespace Lojinha.Cart.API.Entities;


public class Cart 
{
    public List<CartDetail> CartDetails { get; set; } = new();
}