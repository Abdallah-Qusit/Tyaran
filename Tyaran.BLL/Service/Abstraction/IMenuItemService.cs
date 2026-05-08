using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.Services.Abstraction;

public interface IMenuItemService
{
    Task<List<MenuItem>> GetAllAsync();

    Task<MenuItem?> GetByIdAsync(int id);

    Task CreateAsync(MenuItem menuItem);

    Task UpdateAsync(MenuItem menuItem);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}