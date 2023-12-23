
using AutoMapper;

namespace Lojinha.Product.API.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;
    
    private readonly IMapper _mapper;

    public ItemService(IItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ItemVO> Create(ItemVO vo)
    {
        vo.Id = default;
        var model = await _repository.Create(_mapper.Map<Item>(vo));
        return _mapper.Map<ItemVO>(model);
    }

    public async Task<bool> Delete(long id)
    {
        var item = await _repository.FindById(id);

        ArgumentNullException.ThrowIfNull(item, $"Não há item com Id {id}");

        return await _repository.Delete(id);
    }

    public async Task<IEnumerable<ItemVO>> FindAll()
    {
        var items = await _repository.FindAll();
        return _mapper.Map<List<ItemVO>>(items);
    }

    public async Task<ItemVO> FindById(long id)
    {
        var item = await _repository.FindById(id);

        ArgumentNullException.ThrowIfNull(item, $"Não há item com Id {id}");

        return _mapper.Map<ItemVO>(await _repository.FindById(id));
    }

    public async Task<ItemVO> Update(int id, ItemVO vo)
    {
        var item = await _repository.FindById(id);

        ArgumentNullException.ThrowIfNull(item, $"Não há item com Id {id}");

        vo.Id = id;

        _mapper.Map(vo, item);

        return _mapper.Map<ItemVO>(await _repository.Update(item));
    }
}