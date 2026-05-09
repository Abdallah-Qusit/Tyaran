using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.Service.Abstraction
{
    public interface IUserHomeService
    {
        Task<List<MenuItem>> GetMostOrderedAsync();
        Task<List<Restaurant>> GetMostVisitedAsync();
        Task<List<MenuItem>> GetRecommendedAsync(int userId);
    }
}
