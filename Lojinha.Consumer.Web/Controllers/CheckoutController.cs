using System.IdentityModel.Tokens.Jwt;
using Lojinha.Consumer.Web.Models;
using Lojinha.Consumer.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Consumer.Web.Controllers;

public class CheckoutController : Controller
{

    private readonly ICartService _cartService;

    private readonly ICouponService _couponService;

    public CheckoutController(ICartService cartService, ICouponService couponService)
    {
        _cartService = cartService;
        _couponService = couponService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string userId)
    {   
        var response = await _cartService.FindCartByUserId(userId);

        foreach (var cartDetail in response.CartDetails)
        {
            Coupon coupon;

            var total = cartDetail.Item!.Price *  cartDetail.Quantity;

            if (!string.IsNullOrEmpty(cartDetail.CouponCode))
            {
               coupon = await _couponService.GetCoupon(cartDetail.CouponCode);

                if (coupon is not null)
                {
                    cartDetail.Discount = coupon.Discount;
                    total -= total * coupon.Discount / 100;
                }
            }
            

            response.Total += total;
        }

        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(Cart cart)
    {   
        await _cartService.CheckoutCart(cart);

        return View("Confirmation");
    }
}