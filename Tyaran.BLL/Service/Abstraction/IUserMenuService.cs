using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.Service.Abstraction
{
    public interface IUserMenuService
    {
        Task<List<MenuItem>> GetRestaurantMenuAsync(int restaurantId);
    }
}
