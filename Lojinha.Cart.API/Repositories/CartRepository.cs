using AutoMapper;
using Lojinha.Cart.API.Context;
using Lojinha.Cart.API.Dtos;
using Lojinha.Cart.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Cart.API.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public CartRepository(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ApplyCouponAsync(long cartDetailId, string coupon)
    {
        var cartDetail = await FindCartDetailById(cartDetailId);
        cartDetail.AddCoupon(coupon);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ClearAsync(string userId)
    {
        var cart = await _context.Carts.SingleOrDefaultAsync(c => c.UserId == userId) ??
         throw new Exception($"Unable to find the cart with the specified user Id {userId}");
        _context.CartDetails.RemoveRange(cart.CartDetails);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<CartDTO> FindByUserIdAsync(string userId)
    {
        var cart = await _context.Carts.Include(c => c.CartDetails).ThenInclude(c => c.Item).AsNoTracking().FirstOrDefaultAsync(c => c.UserId == userId && !c.Finished) ??
            throw new Exception($"Unable to find the cart with the specified user Id {userId}");

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> RemoveAsync(string userId)
    {
        _context.Carts.Remove(await FindCartByUserId(userId));
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveCouponAsync(long cartDetailId)
    {
        var cartDetail = await FindCartDetailById(cartDetailId);
        cartDetail.AddCoupon(string.Empty);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveCartDetailsAsync(long cartDetailId)
    {
        _context.CartDetails.Remove(await FindCartDetailById(cartDetailId));
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<CartDTO> SaveOrUpdateAsync(CartDTO record, string userId)
    {

        using var transaction = await _context.Database.BeginTransactionAsync();

        try 
        {
            var cart = _mapper.Map<Entities.Cart>(record);

            if (!ExistsCart(userId)) // Se existir o produto eu adiciono os Cart Details referentes 
            {   

                cart.Id = default;
                cart.UserId = userId;
                await _context.Carts.AddAsync(cart);
            }
            else 
            {
                cart = (await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && !c.Finished))!; 

                foreach (var cartDetailDTO in record.CartDetails)
                {
                    var cartDetailRecovered = await _context.CartDetails.FirstOrDefaultAsync(
                        c => c.ItemId.Equals(cartDetailDTO.ItemId) && c.CartId.Equals(cart.Id));


                    if (cartDetailRecovered is not null)
                    {
                        cartDetailRecovered.PlusQuantity(cartDetailDTO.Quantity);
                    }
                    else 
                    {
                        var cartDetail = _mapper.Map<CartDetail>(cartDetailDTO);
                        cartDetail.Id = default;
                        cartDetail.CartId = cart.Id;
                        
                        if (ExistsItem(cartDetailDTO.ItemId))
                            cartDetail.Item = null;
                        
                        await _context.CartDetails.AddAsync(cartDetail);
                    }
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return _mapper.Map<CartDTO>(cart);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> CheckoutAsync(string userId)
    {
        var cart = await FindCartByUserId(userId);
        cart.ConfirmCheckout();
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<CartDetail> FindCartDetailById(long id)
    {
        return await _context.CartDetails.FirstOrDefaultAsync(cd => cd.Id == id) ??
            throw new Exception($"Unable to find the cart detail with the specified Id {id}");
    }

    private async Task<Entities.Cart> FindCartByUserId(string userId)
    {
        return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && !c.Finished) ??
            throw new Exception($"Unable to find the cart with the specified user Id {userId}");
    }

    private bool ExistsCart(string id)
    {
        return _context.Carts.Any(c => c.UserId == id && !c.Finished);
    }

    private bool ExistsItem(long id)
    {
        return _context.Items.Any(i => i.Id == id);
    }
}