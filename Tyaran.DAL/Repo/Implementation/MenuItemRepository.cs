using Microsoft.EntityFrameworkCore;
using Tyaran.DAL.Database;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repositories.Abstraction;

namespace Tyaran.DAL.Repositories.Implementation;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly TyaranDbContext _context;

    public MenuItemRepository(TyaranDbContext context)
    {
        _context = context;
    }

    public async Task<List<MenuItem>> GetAllAsync()
    {
        return await _context.MenuItems
            .Include(m => m.MenuCat)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItems
            .Include(m => m.MenuCat)
            .FirstOrDefaultAsync(m =>
                m.ItemId == id);
    }

    public async Task AddAsync(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MenuItem menuItem)
    {
        _context.MenuItems.Update(menuItem);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var menuItem =
            await _context.MenuItems.FindAsync(id);

        if (menuItem != null)
        {
            _context.MenuItems.Remove(menuItem);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.MenuItems
            .AnyAsync(e =>
                e.ItemId == id);
    }
}