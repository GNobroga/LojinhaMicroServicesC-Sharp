namespace Lojinha.Product.API.Services;

public interface IItemService 
{
    public Task<IEnumerable<ItemVO>> FindAll();

    public Task<ItemVO> FindById(long id);

    public Task<bool> Delete(long id);

    public Task<ItemVO> Update(int id, ItemVO vo);

    public Task<ItemVO> Create(ItemVO vo);
}