using Microsoft.EntityFrameworkCore;
using Tyaran.DAL.Database;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repositories.Abstraction;

namespace Tyaran.DAL.Repositories.Implementation;

public class MenuCategoryRepository : IMenuCategoryRepository
{
    private readonly TyaranDbContext _context;

    public MenuCategoryRepository(TyaranDbContext context)
    {
        _context = context;
    }

    public async Task<List<MenuCategory>> GetAllAsync()
    {
        return await _context.MenuCategories
            .ToListAsync();
    }

    public async Task<MenuCategory?> GetByIdAsync(int id)
    {
        return await _context.MenuCategories
            .FirstOrDefaultAsync(m =>
                m.MenuCatId == id);
    }

    public async Task AddAsync(MenuCategory menuCategory)
    {
        _context.MenuCategories.Add(menuCategory);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MenuCategory menuCategory)
    {
        _context.MenuCategories.Update(menuCategory);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var menuCategory =
            await _context.MenuCategories.FindAsync(id);

        if (menuCategory != null)
        {
            _context.MenuCategories.Remove(menuCategory);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.MenuCategories
            .AnyAsync(e =>
                e.MenuCatId == id);
    }
}