using Lojinha.Cart.API.Dtos;

namespace Lojinha.Cart.API.Repositories;

public interface ICartRepository
{
    Task<CartDTO> FindByUserIdAsync(string userId);
    Task<CartDTO> SaveOrUpdateAsync(CartDTO record, string userId);
    Task<bool> RemoveAsync(string userId);

    Task<bool> RemoveCartDetailsAsync(long cartDetailId);
    Task<bool> ApplyCouponAsync(long cartDetailId, string couponCode);
    Task<bool> RemoveCouponAsync(long cartDetailId);
    Task<bool> ClearAsync(string userId);
    Task<bool> CheckoutAsync(string userId);
}