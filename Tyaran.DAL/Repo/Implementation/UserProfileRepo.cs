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
    public class UserProfileRepo : IUserProfileRepo
    {
        TyaranDbContext _context;
        public UserProfileRepo( TyaranDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetProfileAsync(int userId)
        {
            return await _context.Users.FromSqlRaw(
            "EXEC User_Read @UserID",
            new SqlParameter("@UserID", userId)
        )
        .FirstOrDefaultAsync();
        }
        public async Task UpdateProfileAsync(int userId, string firstName, string lastName, string Email)
        {
            await _context.Database.ExecuteSqlRawAsync(
        @"EXEC UserProfile_Update
            @UserID,
            @FirstName,
            @LastName,
            @Email",
            new SqlParameter("@UserID", userId),
            new SqlParameter("@FirstName", firstName),
            new SqlParameter("@LastName", lastName),
            new SqlParameter("@Email", Email)
    );
        }
    }
}
