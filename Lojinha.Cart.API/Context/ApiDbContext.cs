using Lojinha.Cart.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Cart.API.Context;

public class ApiDbContext : DbContext
{
    private IConfiguration _configuration;
    public DbSet<Item> Items { get; set; }

    public DbSet<CartDetail> CartDetails { get; set; }

    public ApiDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }
}