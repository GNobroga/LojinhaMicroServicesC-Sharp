using System.IdentityModel.Tokens.Jwt;
using Lojinha.Consumer.Web.Models;
using Lojinha.Consumer.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Consumer.Web.Controllers;

public class CartController : Controller
{
    private readonly IItemService _itemService;

    private readonly ICartService _cartService;

    private readonly ICouponService _couponService;

    public CartController(IItemService itemService, ICartService cartService, ICouponService couponService)
    {
        _itemService = itemService;
        _cartService = cartService;
        _couponService = couponService;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (userId is null) return RedirectToAction("List", "Index");        
        var response = await _cartService.FindCartByUserId(userId);

        foreach (var cartDetail in response.CartDetails)
        {
            Coupon coupon = default;
            var total = cartDetail.Item!.Price *  cartDetail.Quantity;

            if (!string.IsNullOrEmpty(cartDetail.CouponCode))
            {
               coupon = await _couponService.GetCoupon(cartDetail.CouponCode);
                if (coupon is not null)
                {
                    total -= total * coupon.Discount / 100;
                }
            }
            

            response.Total += total;
        }
        return View(response);
    }

    public async Task<IActionResult> Remove(long id)
    {
        var response = await _cartService.RemoveItemFromCart(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> RemoveCoupon(long cartDetailId)
    {
        var response = await _cartService.RemoveCoupon(cartDetailId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<ActionResult> ApplyCoupon(long cartDetailId, CartDetail cartDetail)
    {   
        if (string.IsNullOrEmpty(cartDetail.CouponCode)) return RedirectToAction(nameof(Index));
        var response = await _cartService.ApplyCoupon(cartDetailId, cartDetail.CouponCode!);
        return RedirectToAction(nameof(Index));
    }

}