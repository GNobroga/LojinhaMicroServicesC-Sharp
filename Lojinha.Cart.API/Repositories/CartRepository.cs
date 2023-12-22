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
        var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.Id == cartDetailId) ??
            throw new Exception($"Unable to find the cart detail with the specified Id {cartDetailId}");

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
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && !c.Finished);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> RemoveAsync(long cartId)
    {
        var cartDetail = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId && !c.Finished) ??
            throw new Exception($"Unable to find the cart with the specified Id {cartId}");

        cartDetail.Finished = !cartDetail.Finished;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveCouponAsync(long cartDetailId)
    {
        var cartDetail = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.Id == cartDetailId) ??
            throw new Exception($"Unable to find the cart detail with the specified Id {cartDetailId}");
        
        cartDetail.AddCoupon(string.Empty);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<CartDTO> SaveOrUpdateAsync(CartDTO record, string userId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try 
        {
            var cart = _mapper.Map<Entities.Cart>(record);

            if (!ExistsCart(record.Id))
            {
                if (!record.CartDetails.All(c => ExistsItem(c.ItemId)))
                {
                    throw new Exception("Some products does not exist. Make sure they are registred");
                }

                await _context.Carts.AddAsync(cart);
            }
            else 
            {
                cart = (await _context.Carts.FindAsync(record.Id))!; 

                foreach (var cartDetailDTO in record.CartDetails)
                {
                    var cartDetailRecovered = cart.CartDetails.FirstOrDefault(c => c.ItemId == cartDetailDTO.ItemId);

                    cartDetailRecovered?.PlusQuantity(cartDetailDTO.Quantity);

                    if (ExistsItem(cartDetailDTO.ItemId))
                    {
                        throw new Exception($"There is no Item with the specified Id {cartDetailDTO.ItemId}");
                    }

                    if (cartDetailRecovered is null)
                    {
                        var cartDetail = _mapper.Map<CartDetail>(cartDetailDTO);

                        cartDetail.Id = default;

                        cart.CartDetails.Add(cartDetail);
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

    private bool ExistsCart(long id)
    {
        return _context.Carts.Any(c => c.Id == id);
    }

    private bool ExistsItem(long id)
    {
        return _context.Items.Any(i => i.Id == id);
    }
}