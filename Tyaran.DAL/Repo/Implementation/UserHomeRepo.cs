using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Database;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repo.Abstraction;

namespace Tyaran.DAL.Repo.Implementation
{
    public class UserHomeRepo : IUserHomeRepo
    {
        private readonly TyaranDbContext _context;

        public UserHomeRepo(TyaranDbContext context)
        {
            _context = context;
        }
        public async Task<List<MenuItem>> GetMostOrderedAsync()
        {
            return await _context.MenuItems
                .FromSqlRaw("EXEC Home_MostOrdered")
                .ToListAsync();
        }
        public async Task<List<Restaurant>> GetMostVisitedAsync()
        {
            return await _context.Restaurants
                .FromSqlRaw("EXEC Home_MostVisited")
                .ToListAsync();
        }
        public async Task<List<MenuItem>> GetRecommendedAsync(int userId)
        {
            var param = new SqlParameter("@UserID", userId);
            return await _context.MenuItems
                .FromSqlRaw("EXEC Home_Recommended @UserID", param)
                .ToListAsync();
        }
    }
}
