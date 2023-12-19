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

    public async Task<bool> ApplyCouponAsync(long cartDetailId, string userId, string couponCode)
    {
        var cartDetail = _context.CartDetails.FirstOrDefault(cd => cd.Id == cartDetailId && cd.UserId == userId && !cd.Paid);

        if (cartDetail != null)
        {
            cartDetail.CouponCode = couponCode;
            _context.CartDetails.Update(cartDetail);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<bool> ClearAsync(string userId)
    {
        var cartDetails = _context.CartDetails.Where(cd => cd.UserId == userId && !cd.Paid);

        _context.CartDetails.RemoveRange(cartDetails);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<CartDTO> FindByUserIdAsync(string userId)
    {
        var cartDetails = await _context.CartDetails
            .Where(cd => cd.UserId == userId && !cd.Paid)
            .Include(cd => cd.Item)
            .AsNoTracking()
            .ToListAsync();
        
        return _mapper.Map<CartDTO>(new Entities.Cart { CartDetails = cartDetails });
    }

    public async Task<bool> RemoveAsync(long cartDetailId, string userId)
    {
        var cartDetail = _context.CartDetails.FirstOrDefault(cd => cd.UserId == userId && cd.Id == cartDetailId && !cd.Paid);

        if (cartDetail != null)
        {
            _context.CartDetails.Remove(cartDetail);
            return await _context.SaveChangesAsync() > 0;
        }
        
        return false;
    }

    public async Task<bool> RemoveCouponAsync(long cartDetailId, string userId)
    {
       var cartDetail = _context.CartDetails.FirstOrDefault(cd => cd.UserId == userId && cd.Id == cartDetailId && !cd.Paid);

       if (cartDetail != null) 
        {
            cartDetail.CouponCode = null;
            _context.CartDetails.Update(cartDetail);
            return await _context.SaveChangesAsync() > 0;
        }

        return false;
    }

    public async Task<CartDTO> SaveOrUpdateAsync(CartDTO record, string userId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try 
        {
            foreach(var cartDetail in record.CartDetails)
            {   
                var data = _context.CartDetails.Find(cartDetail.Id);
                if (data != null)
                {
                     _context.CartDetails.Update(_mapper.Map(cartDetail, data));
                }
                else 
                {
                    if (cartDetail.Item?.Id == null || _context.Items.Find(cartDetail.Item.Id) == null)
                        throw new Exception("É necessário que o produto exista para ser posto no carrinho.");

                    var cartDetailsSimilar = await _context.CartDetails.FirstOrDefaultAsync(cd => cd.ItemId == cartDetail.Item.Id && cd.UserId == userId && !cd.Paid);

                    if (cartDetailsSimilar != null)
                    {
                        cartDetailsSimilar.Count += cartDetail.Count;
                    }
                    else 
                    {
                        await _context.CartDetails.AddAsync(_mapper.Map<CartDetailDTO, CartDetail>(cartDetail));
                    }
                }
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return await FindByUserIdAsync(userId);
        } 
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new ApplicationException("Um erro ocorreu durante o salvar: " + ex.Message);
        }
    }
}