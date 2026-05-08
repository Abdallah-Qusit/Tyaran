using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repositories.Abstraction;

public interface IMenuCategoryRepository
{
    Task<List<MenuCategory>> GetAllAsync();

    Task<MenuCategory?> GetByIdAsync(int id);

    Task AddAsync(MenuCategory menuCategory);

    Task UpdateAsync(MenuCategory menuCategory);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}