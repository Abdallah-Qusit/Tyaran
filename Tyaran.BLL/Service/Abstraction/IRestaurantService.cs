using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.Services.Abstraction;

public interface IRestaurantService
{
    Task<List<Restaurant>> GetAllAsync();

    Task<Restaurant?> GetByIdAsync(int id);

    Task CreateAsync(Restaurant restaurant);

    Task UpdateAsync(Restaurant restaurant);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}