using System.IdentityModel.Tokens.Jwt;
using Lojinha.Consumer.Web.Models;
using Lojinha.Consumer.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Consumer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IItemService _itemService;

        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IItemService itemService, ICartService cartService)
        {
            _logger = logger;
            _itemService = itemService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {

            var items = await _itemService.FindAll();

            return View(items);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemService.FindById(id);
            return View(item);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Details(ItemModel model)
        {

            var userId = User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
            var item = await _itemService.FindById(model.Id);

            Cart cart = new()
            {
                UserId = userId,
                CartDetails = {
                     new()
                        {
                            Item = item,
                            ItemId = item.Id,
                            Quantity = model.Count
                        }
                }
            };

            await _cartService.AddItemToCart(cart, userId);

            TempData["Message"] = "Pedido adicionado no carrinho";
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}