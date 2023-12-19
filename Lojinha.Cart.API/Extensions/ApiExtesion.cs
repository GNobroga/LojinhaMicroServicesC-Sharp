
using AutoMapper;
using Lojinha.Cart.API.Dtos;
using Lojinha.Cart.API.Dtos.Profiles;

namespace Lojinha.Cart.API.Extensions;

public static class ApiExtension
{
    public static WebApplication AddApiEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("carrinho");

        api.MapPost("/", (HttpContext context, CartDTO dto, IMapper mapper) => {
            return Results.Ok(mapper.Map<Entities.Cart>(dto));
        });

        return app;
    }

    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(opt => {
            opt.AddProfile<MappingProfile>();
        });
    }
}