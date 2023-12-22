
using AutoMapper;
using FluentValidation;
using Lojinha.Cart.API.Context;
using Lojinha.Cart.API.Dtos;
using Lojinha.Cart.API.Dtos.Filters;
using Lojinha.Cart.API.Dtos.Profiles;
using Lojinha.Cart.API.Dtos.Validators;
using Lojinha.Cart.API.Entities;
using Lojinha.Cart.API.Repositories;
using Lojinha.Cart.API.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Cart.API.Extensions;

public static class ApiExtension
{
    public static WebApplication AddApiEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("carts");

        api.MapGet("/", async (HttpContext context, [FromQuery] string userId, ICartRepository repository) => {
            return Results.Ok(await repository.FindByUserIdAsync(userId));
        });

        return app;
    }

    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(opt => opt.AddProfile<MappingProfile>());

        builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IValidator<CartDetailDTO>, CartDetailDTOValidator>();
        builder.Services.AddScoped<IValidator<ItemDTO>, ItemDTOValidator>();
        builder.Services.AddScoped<IValidator<CartDTO>, CartDTOValidator>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
    }

    public static void ApiHandlerException(this WebApplication app)
    {
        app.UseExceptionHandler(map => {

            map.Run(async context => {

                var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var requestFailureResponse = new RequestFailureResponse();

                requestFailureResponse.Message = error?.Message != null ?  error.Message : requestFailureResponse.Message;
               
                await context.Response.WriteAsJsonAsync(new RequestFailureResponse());
            });
        });
    }
}