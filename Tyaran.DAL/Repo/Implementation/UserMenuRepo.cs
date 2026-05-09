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
    public class UserMenuRepo : IUserMenuRepo
    {
        private readonly TyaranDbContext _context;
        public UserMenuRepo(TyaranDbContext context) {
            _context = context;
        }
        public async Task<List<MenuItem>> GetRestaurantMenuAsync(int restaurantId)
        {
            var param = new SqlParameter("@RestaurantID", restaurantId);
            return await _context.MenuItems
                .FromSqlRaw("EXEC sp_Restaurant_GetMenu @RestaurantID", param)
                .ToListAsync();
        }
    }
}
