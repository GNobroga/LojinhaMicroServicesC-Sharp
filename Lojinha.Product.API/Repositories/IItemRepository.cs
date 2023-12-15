

namespace Lojinha.Product.API.Repositories;

public interface IItemRepository
{
    Task<IEnumerable<Item>> FindAll();

    Task<Item> FindById(long id);

    Task<Item> Create(Item entity);

    Task<Item> Update(Item entity);

    Task<bool> Delete(long id);
}