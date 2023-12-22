using Lojinha.Cart.API.Dtos;

namespace Lojinha.Cart.API.Repositories;

public interface ICartRepository
{
    public Task<CartDTO> FindByUserIdAsync(string userId);
    public Task<CartDTO> SaveOrUpdateAsync(CartDTO record, string userId);
    public Task<bool> RemoveAsync(string userId);

    public Task<bool> RemoveCartDetailsAsync(long cartDetailId);
    public Task<bool> ApplyCouponAsync(long cartDetailId, string couponCode);
    public Task<bool> RemoveCouponAsync(long cartDetailId);
    public Task<bool> ClearAsync(string userId);
}