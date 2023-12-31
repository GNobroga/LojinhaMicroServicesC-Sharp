
using FluentValidation;
using Lojinha.Cart.API.Context;
using Lojinha.Cart.API.Dtos;
using Lojinha.Cart.API.Dtos.Filters;
using Lojinha.Cart.API.Dtos.Profiles;
using Lojinha.Cart.API.Dtos.Validators;
using Lojinha.Cart.API.Messages;
using Lojinha.Cart.API.RabbitMQSender;
using Lojinha.Cart.API.Repositories;
using Lojinha.Cart.API.Utils;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Cart.API.Extensions;

public static class ApiExtension
{
    public static WebApplication ConfigureAPIEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("api/v1/carts");

        api.MapGet("find-cart/{userId}", async (HttpContext context, [FromRoute] string userId, ICartRepository repository) => {
            return Results.Ok(await repository.FindByUserIdAsync(userId));
        });

        api.MapPost("add-cart/{userId}", async (HttpContext context, [FromRoute] string userId, [FromBody] CartDTO cartDTO, ICartRepository repository) => {
            return Results.Created(
                string.Concat(context.Request.GetDisplayUrl(), $"?userId={userId}"), 
                await repository.SaveOrUpdateAsync(cartDTO, userId));
        }).AddEndpointFilter<DTOValidatorFilter<CartDTO>>();

        api.MapPut("update-cart/{userId}", async (HttpContext context, [FromRoute] string userId, [FromBody] CartDTO cartDTO, ICartRepository repository) => {
            return Results.Ok(await repository.SaveOrUpdateAsync(cartDTO, userId));
        }).AddEndpointFilter<DTOValidatorFilter<CartDTO>>();

        
        api.MapDelete("remove-cart/{userId}", async (HttpContext context, [FromRoute] string userId, ICartRepository repository) => {
            return Results.Ok(await repository.RemoveAsync(userId));
        });
         
        api.MapDelete("remove-cart-detail/{id:long}", async (HttpContext context, [FromRoute] long id, ICartRepository repository) => {
            return Results.Ok(await repository.RemoveCartDetailsAsync(id));
        });

           
        api.MapGet("add-cupom/{id:long}/{couponCode}", async (HttpContext context, [FromRoute] long id, [FromRoute] string couponCode, ICartRepository repository) => {
            return Results.Ok(await repository.ApplyCouponAsync(id, couponCode));
        });

        api.MapGet("remove-cupom/{id:long}", async (HttpContext context, [FromRoute] long id, ICartRepository repository) => {
            return Results.Ok(await repository.RemoveCouponAsync(id));
        });


        api.MapPost("checkout", async (
            HttpContext context, 
            [FromBody] CheckoutDTO dto, 
            ICartRepository repository,
            IRabbitMQMessageSender rabbit
        ) => {
            
            var cart = await repository.FindByUserIdAsync(dto.UserId!);
            if (cart is null) return Results.NotFound();
            dto.CartDetails = cart.CartDetails;
            dto.DateTime = new();

            rabbit.Send(dto, "checkout_queue");
            
            return Results.Ok(dto);
        });


        return app;
    }

    public static void ConfigureAPIServices(this WebApplicationBuilder builder)
    {   
        builder.Services.AddCors();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(opt => opt.AddProfile<MappingProfile>());

        builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IValidator<CartDetailDTO>, CartDetailDTOValidator>();
        builder.Services.AddScoped<IValidator<ItemDTO>, ItemDTOValidator>();
        builder.Services.AddScoped<IValidator<CartDTO>, CartDTOValidator>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
    }

    public static void ApiHandlerException(this WebApplication app)
    {
        app.UseExceptionHandler(map => {

            map.Run(async context => {

                var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var requestFailureResponse = new RequestFailureResponse();

                requestFailureResponse.Message = !string.IsNullOrEmpty(error?.Message) ?  error.Message : requestFailureResponse.Message;
               
                await context.Response.WriteAsJsonAsync(requestFailureResponse);
            });
        });
    }
}