using Microsoft.AspNetCore.Http;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.Services.Abstraction;

public interface IMenuCategoryService
{
    Task<List<MenuCategory>> GetAllAsync();

    Task<MenuCategory?> GetByIdAsync(int id);

    Task CreateAsync(MenuCategory menuCategory);

    Task UpdateAsync(MenuCategory menuCategory);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
     

}