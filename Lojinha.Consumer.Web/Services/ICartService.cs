using Lojinha.Consumer.Web.Models;

namespace Lojinha.Consumer.Web.Services;

public interface ICartService
{
    Task<Cart> FindCartByUserId(string userId);

    Task<Cart> AddItemToCart(Cart cart, string userId);

    Task<Cart> UpdateCart(Cart cart, string userId);

    Task<bool> RemoveItemFromCart(long cartDetailId);

    Task<bool> RemoveCart(string userId);

    Task<bool> ApplyCoupon(long cartDetailId, string couponCode);
    Task<bool> RemoveCoupon(long cartDetailId);
    Task<bool> ClearCart(string userId);
    Task<bool> CheckoutCart(string userId);
}