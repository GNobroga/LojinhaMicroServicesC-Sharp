using System.IdentityModel.Tokens.Jwt;
using Lojinha.Consumer.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Consumer.Web.Controllers;

public class CartController : Controller
{
    private readonly IItemService _itemService;

    private readonly ICartService _cartService;

    public CartController(IItemService itemService, ICartService cartService)
    {
        _itemService = itemService;
        _cartService = cartService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        if (userId is null) return RedirectToAction("List", "Index");        
        var response = await _cartService.FindCartByUserId(userId);
        
        response.Total = response.CartDetails.Sum(c => c.Item.Price *  c.Quantity);
        return View(response);
    }
}