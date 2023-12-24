using Lojinha.Order.API.Context;
using Lojinha.Order.API.RabbitMQ;
using Lojinha.Order.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var dbContextBuilder = new DbContextOptionsBuilder<ApiDbContext>();

dbContextBuilder.UseSqlite("Data source=application.db");

builder.Services.AddSingleton(new OrderRepository(dbContextBuilder.Options));

// Mantém o serviço ativo
builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
