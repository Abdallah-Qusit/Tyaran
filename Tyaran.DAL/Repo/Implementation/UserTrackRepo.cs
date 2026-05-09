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
    public class UserTrackRepo : IUserTrackRepo
    {
        private readonly TyaranDbContext _context;
        public UserTrackRepo( TyaranDbContext context)
        {
            _context = context;
        }
        public async Task<Order?> TrackOrderAsync(int orderId)
        {
            return await _context.Orders.FromSqlRaw(
            "EXEC Order_Track @OrderID",
            new SqlParameter("@OrderID", orderId))
            .Include(o => o.Address)
            .Include(o => o.Restaurant)
                .ThenInclude(r => r.Address)
            .FirstOrDefaultAsync();
        }
    }
}
