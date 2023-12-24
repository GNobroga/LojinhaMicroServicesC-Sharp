using Lojinha.Order.API.Entities;

namespace Lojinha.Order.API.Repositories;

public interface IOrderRepository
{
    Task<bool> AddAsync(OrderEntity order);

    Task<bool> UpdateStatus(long orderId, bool paid);

}