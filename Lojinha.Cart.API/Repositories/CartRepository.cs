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

    public async Task<bool> ApplyCouponAsync()
    {
       

        return false;
    }

    public async Task<bool> ClearAsync(string userId)
    {
      
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<CartDTO> FindByUserIdAsync(string userId)
    {
        
        
        return _mapper.Map<CartDTO>(new Entities.Cart { CartDetails = cartDetails });
    }

    public async Task<bool> RemoveAsync()
    {
      
        
        return false;
    }

    public async Task<bool> RemoveCouponAsync()
    {
     
        return false;
    }

    public async Task<CartDTO> SaveOrUpdateAsync(CartDTO record, string userId)
    {
        // Needs Implementation
    }
}