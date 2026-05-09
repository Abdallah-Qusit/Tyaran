using Tyaran.DAL.Entities.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tyaran.DAL.Repo.Abstraction
{
    public interface IUserMenuRepo
    {
        Task<List<MenuItem>> GetRestaurantMenuAsync(int restaurantId);
    }
}
