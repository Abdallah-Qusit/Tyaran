using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repo.Abstraction
{
    public interface IUserProfileRepo
    {
        Task<User?> GetProfileAsync(int userId);
        Task UpdateProfileAsync(int userId, string firstName, string lastName, string Email);
    }
}
