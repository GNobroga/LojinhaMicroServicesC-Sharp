using AutoMapper;
using Lojinha.Coupon.API.Context;
using Lojinha.Coupon.API.ObjectValues;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Coupon.API.Repositores;

public class CouponRepository : ICouponRepository
{
    private readonly IMapper _mapper;

    private readonly AppDbContext _context;

    public CouponRepository(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CouponVO> GetByCode(string code)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == code);
        return _mapper.Map<CouponVO>(coupon);
    }
}