using Lojinha.Cart.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Cart.API.Context;

public class ApiDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }

    public DbSet<CartDetail> CartDetails { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {}
}