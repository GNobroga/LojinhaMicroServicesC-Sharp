using Lojinha.Cart.API.Dtos;

namespace Lojinha.Cart.API.Repositories;

public interface ICartRepository
{
    public Task<CartDTO> FindByUserIdAsync(string userId);
    public Task<CartDTO> SaveOrUpdateAsync(CartDTO record, string userId);
    public Task<bool> RemoveAsync(long cartDetailId, string userId);
    public Task<bool> ApplyCouponAsync(long cartDetailId, string userId, string couponCode);
    public Task<bool> RemoveCouponAsync(long cartDetailId, string userId);
    public Task<bool> ClearAsync(string userId);
}