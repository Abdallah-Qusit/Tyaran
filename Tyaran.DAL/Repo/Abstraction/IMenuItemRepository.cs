using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repositories.Abstraction;

public interface IMenuItemRepository
{
    Task<List<MenuItem>> GetAllAsync();

    Task<MenuItem?> GetByIdAsync(int id);

    Task AddAsync(MenuItem menuItem);

    Task UpdateAsync(MenuItem menuItem);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}