using System.Collections.ObjectModel;
using Lojinha.Consumer.Web.Models;
using Lojinha.Consumer.Web.Services;
using Lojinha.Consumer.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Consumer.Web.Controllers;

public class ItemController : Controller
{
    private readonly IItemService _service;

    public ItemController(IItemService service) => _service = service;

    [Authorize(Roles = Role.CLIENT)]
    public async Task<ViewResult> List() 
    {
        var items = await _service.FindAll();

        return View(items);
    }

    [Authorize(Roles = Role.CLIENT)]
    public IActionResult Create() => View();

    [Authorize(Roles = Role.CLIENT)]
    [HttpPost]
    public async Task<IActionResult> Create(ItemModel model) 
    {   
        if (!ModelState.IsValid) return View(model);

        await _service.Create(model);

        return RedirectToAction("List");
    }

    [Authorize(Roles = Role.CLIENT)]
    public async Task<IActionResult> Update(int id) 
    {
        var item = await _service.FindById(id);

        return View(item);
    }

    [Authorize(Roles = Role.CLIENT)]
    [HttpPost]
    public async Task<IActionResult> Update(ItemModel model) 
    {   
        if (!ModelState.IsValid) return View(model);

        await _service.Update(model.Id, model);

        return RedirectToAction("List");
    }

    [Authorize(Roles = Role.ADMIN)]
    public async Task<IActionResult> Delete(long id) 
    {   
        await _service.Delete(id);

        return RedirectToAction("List");
    }

    public string Error()
    {
        return "Ocorreu um erro";
    }
}