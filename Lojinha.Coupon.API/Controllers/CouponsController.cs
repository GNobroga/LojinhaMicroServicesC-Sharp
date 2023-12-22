using Lojinha.Coupon.API.ObjectValues;
using Lojinha.Coupon.API.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Coupon.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public  class CouponsController : ControllerBase
{   

    private readonly ICouponRepository _repository;

    public CouponsController(ICouponRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<CouponVO>> Get(string code)
    {
        try 
        {
            var coupon = await _repository.GetByCode(code);
            return coupon;
        }
        catch (Exception ex)
        {
            return BadRequest(new {
                Title = "Error",
                ex.Message
            });
        }
    }
}