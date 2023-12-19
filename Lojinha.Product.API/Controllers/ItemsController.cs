using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lojinha.Product.API.Controllers;

[Produces(contentType: "application/json")]
[Consumes(contentType: "application/json")]
[ApiController]
[Route("api/v1/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _service;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IItemService service, ILogger<ItemsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Authorize(Roles = Role.CLIENT)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ItemVO>))]
    public async Task<ActionResult<IEnumerable<ItemVO>>> Get() 
    {
       _logger.LogInformation("### LISTANDO ITEMS ###");
       return Ok(await _service.FindAll());
    }

    [Authorize(Roles = Role.CLIENT)]
    [HttpGet("{id:int}", Name = "FindById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemVO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemVO>> FindById(int id)
    {
        try 
        {
            _logger.LogInformation("### OBTENDO POR ID ###");
            return Ok(await _service.FindById(id));
        } catch (ArgumentNullException ex)
        {
            _logger.LogError("### ERROR AO OBTER POR ID ###");
            return NotFound(new Error { 
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = ex.Message
            });
        }
    }

    [Authorize(Roles = Role.CLIENT)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ItemVO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(ItemVO vo)
    {
        try 
        {
            _logger.LogInformation("### CRIANDO ITEM ###");
            var created = await _service.Create(vo); 
            return CreatedAtAction("FindById", new { Id = created.Id }, created);
        } 
        catch 
        {
            _logger.LogError("### ERRO AO CRIAR ITEM ###");
            return BadRequest();
        }
    }

    [Authorize(Roles = Role.CLIENT)]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ItemVO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(int id, ItemVO vo)
    {
        try 
        {
            _logger.LogInformation("### ATUALIZANDO ITEM ###");
            return Ok(await _service.Update(id, vo));
        } 
        catch (ArgumentNullException ex)
        {
            _logger.LogError("### ERRO AO ATUALIZAR ITEM ###");
             return NotFound(new Error { 
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = ex.Message
            });
        }
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        try 
        {
            _logger.LogInformation("### DELETANDO ITEM ###");
            return Ok(await _service.Delete(id));
        } 
        catch (ArgumentNullException ex)
        {
            _logger.LogError("### ERRO AO DELETAR ITEM ###");
             return NotFound(new Error { 
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = ex.Message
            });
        }
    }

}