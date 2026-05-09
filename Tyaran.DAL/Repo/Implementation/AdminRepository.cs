using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Database;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Enum;
using Tyaran.DAL.Repo.Abstraction;

namespace Tyaran.DAL.Repo.Implementation
{
    public class AdminRepository : IAdminRepository
    {

        private readonly TyaranDbContext _db;

        public AdminRepository(TyaranDbContext db)
        {
            _db = db;
        }

        public Task<int> GetUsersCountAsync() =>
            _db.Users.CountAsync();

        public Task<int> GetDeliveriesCountAsync() =>
            _db.DeliveryMen.CountAsync();

        public Task<int> GetRestaurantsCountAsync() =>
            _db.Restaurants.CountAsync();

        public Task<int> GetOrdersCountAsync() =>
            _db.Orders.CountAsync();

        public async Task<List<(DeliveryMan Delivery, User User)>> GetPendingDeliveriesAsync()
        {
            var deliveries = await _db.DeliveryMen
                .Where(d => d.ApprovalStatus == (int)ApprovalStatusEnum.Pending)
                .ToListAsync();

            var userIds = deliveries.Select(d => d.UserId).ToList();

            var users = await _db.Users
                .Where(u => userIds.Contains(u.UserId))
                .Include(u => u.UserPhones)
                .ToListAsync();

            return deliveries
                .Join(
                    users,
                    d => d.UserId,
                    u => u.UserId,
                    (d, u) => (Delivery: d, User: u)
                )
                .ToList();
        }
        public Task<List<Restaurant>> GetPendingRestaurantsAsync() =>
            _db.Restaurants
               .Where(r => r.ApprovalStatus == (int)ApprovalStatusEnum.Pending)
               .Include(r => r.Owner)
                .Include(r => r.Address)      
                    .Include(r => r.RestaurantPhones)
            .ToListAsync();

        public async Task ApproveDeliveryAsync(int deliveryId)
        {
            var d = await _db.DeliveryMen.FindAsync(deliveryId);
            if (d == null) return;

            d.ApprovalStatus = (int)ApprovalStatusEnum.Approved;
            d.IsAvailable = true;

            await _db.SaveChangesAsync();
        }

        public async Task RejectDeliveryAsync(int deliveryId)
        {
            var d = await _db.DeliveryMen.FindAsync(deliveryId);
            if (d == null) return;

            d.ApprovalStatus = (int)ApprovalStatusEnum.Rejected;
            d.IsAvailable = false;

            await _db.SaveChangesAsync();
        }

        public async Task ApproveRestaurantAsync(int restaurantId)
        {
            var r = await _db.Restaurants.FindAsync(restaurantId);
            if (r == null) return;

            r.ApprovalStatus = (int)ApprovalStatusEnum.Approved;
            r.IsActive = true;

            await _db.SaveChangesAsync();
        }

        public async Task RejectRestaurantAsync(int restaurantId)
        {
            var r = await _db.Restaurants.FindAsync(restaurantId);
            if (r == null) return;

            r.ApprovalStatus = (int)ApprovalStatusEnum.Rejected;
            r.IsActive = false;

            await _db.SaveChangesAsync();
        }


    }
}
