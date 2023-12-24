using Lojinha.Order.API.Context;
using Lojinha.Order.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Order.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DbContextOptions<ApiDbContext> _options;

    public OrderRepository(DbContextOptions<ApiDbContext> options)
    {
        _options = options;
    }

    public async Task<bool> AddAsync(OrderEntity order)
    {
        if (order is null) return false;
        await using var db = new ApiDbContext(_options);
        db.Orders.Add(order);
        return await db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateStatus(long orderId, bool paid)
    {
        await using var db = new ApiDbContext(_options);
        var order = await db.Orders.SingleOrDefaultAsync(o => o.Id == orderId);
        if (order is null) return false;
        order.Status = paid;
        return await db.SaveChangesAsync() > 0;
    }
}