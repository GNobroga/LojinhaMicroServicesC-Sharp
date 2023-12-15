
using Microsoft.EntityFrameworkCore;

namespace Lojinha.Product.API.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Item> Create(Item entity)
    {
        _context.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(long id)
    {
        var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

        if (item is null) return false;

        _context.Entry(item).State = EntityState.Deleted;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Item>> FindAll()
    {
        return await _context.Items.AsNoTracking().ToListAsync();
    }

    public async Task<Item> FindById(long id)
    {
        var item = await _context.Items.FindAsync(id);

        return item!;
    }

    public async Task<Item> Update(Item entity)
    {
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return entity;
    }
}