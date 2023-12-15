using Microsoft.EntityFrameworkCore;

namespace Lojinha.Product.API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Item>().HasData(new Item[] {
            new() { Id = 1, Name = "Celular", CategoryName = "Eletrônicos" },
            new() { Id = 2, Name = "Smartphone", CategoryName = "Eletrônicos" },
            new() { Id = 3, Name = "Computer", CategoryName = "Eletrônicos" }
        });
    }
}