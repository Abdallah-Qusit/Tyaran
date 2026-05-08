using Microsoft.EntityFrameworkCore;
using Tyaran.DAL.Database;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repositories.Abstraction;

namespace Tyaran.DAL.Repositories.Implementation;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly TyaranDbContext _context;

    public RestaurantRepository(TyaranDbContext context)
    {
        _context = context;
    }

    // =========================
    // GET ALL
    // =========================

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants
            .ToListAsync();
    }

    // =========================
    // GET BY ID
    // =========================

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        return await _context.Restaurants
            .FirstOrDefaultAsync(r =>
                r.RestaurantId == id);
    }

    // =========================
    // CREATE
    // =========================

    public async Task AddAsync(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);

        await _context.SaveChangesAsync();
    }

    // =========================
    // UPDATE
    // =========================

    public async Task UpdateAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);

        await _context.SaveChangesAsync();
    }

    // =========================
    // DELETE
    // =========================

    public async Task DeleteAsync(int id)
    {
        var restaurant =
            await _context.Restaurants.FindAsync(id);

        if (restaurant != null)
        {
            _context.Restaurants.Remove(restaurant);

            await _context.SaveChangesAsync();
        }
    }

    // =========================
    // EXISTS
    // =========================

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Restaurants
            .AnyAsync(r =>
                r.RestaurantId == id);
    }
}