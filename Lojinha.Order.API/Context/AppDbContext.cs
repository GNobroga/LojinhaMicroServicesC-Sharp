
using Lojinha.Order.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Order.API.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {}

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }
}