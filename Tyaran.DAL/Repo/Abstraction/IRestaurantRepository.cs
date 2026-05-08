using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repositories.Abstraction;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();

    Task<Restaurant?> GetByIdAsync(int id);

    Task AddAsync(Restaurant restaurant);

    Task UpdateAsync(Restaurant restaurant);

    Task DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}