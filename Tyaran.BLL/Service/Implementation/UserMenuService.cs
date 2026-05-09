using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.BLL.Service.Abstraction;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repo.Abstraction;

namespace Tyaran.BLL.Service.Implementation
{
    public class UserMenuService : IUserMenuService
    {
        private readonly IUserMenuRepo _userMenuRepo;
        public UserMenuService(IUserMenuRepo menuRepository)
        {
            _userMenuRepo = menuRepository;
        }
        public async Task<List<MenuItem>> GetRestaurantMenuAsync(int restaurantId)
        {
            return await _userMenuRepo.GetRestaurantMenuAsync(restaurantId);
        }
    }
}
