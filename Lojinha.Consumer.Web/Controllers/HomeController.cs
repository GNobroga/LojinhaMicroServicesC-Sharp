using Lojinha.Consumer.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Consumer.Web.Controllers;

public class HomeController : Controller
{
    private readonly IItemService _service;

    public HomeController(IItemService service) => _service = service;
    public async Task<ViewResult> Index() 
    {
        var models = await _service.FindAll();
        
        return View(models);
    }
}