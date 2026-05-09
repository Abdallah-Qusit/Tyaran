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
    public class UserHomeService : IUserHomeService
    {
        private readonly IUserHomeRepo _userHomeRepo;
        public UserHomeService(IUserHomeRepo userHomeRepo)
        {
            _userHomeRepo = userHomeRepo;
        }
        public async Task<List<MenuItem>> GetMostOrderedAsync()
        {
            return await _userHomeRepo.GetMostOrderedAsync();
        }
        public async Task<List<Restaurant>> GetMostVisitedAsync()
        {
            return await _userHomeRepo.GetMostVisitedAsync();
        }
        public async Task<List<MenuItem>> GetRecommendedAsync(int userId)
        {
            return await _userHomeRepo.GetRecommendedAsync(userId);
        }
    }
}
