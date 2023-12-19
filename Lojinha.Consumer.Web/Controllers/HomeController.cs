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

        public HomeController(ILogger<HomeController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }
        public async Task<IActionResult> Index()
        {

            var items = await _itemService.FindAll();

            return View(items);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemService.FindById(id);
            return View(item);
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