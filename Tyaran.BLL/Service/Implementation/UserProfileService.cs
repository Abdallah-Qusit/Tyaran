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
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepo _repo;
        public UserProfileService(IUserProfileRepo repo)
        {
            _repo = repo;
        }
        public async Task<User?> GetProfileAsync(int userId)
        {
            return await _repo.GetProfileAsync(userId);
        }
        public async Task UpdateProfileAsync(int userId, string firstName, string lastName, string email)
        {
            await _repo.UpdateProfileAsync(userId,firstName,lastName,email);
        }
    }
}
