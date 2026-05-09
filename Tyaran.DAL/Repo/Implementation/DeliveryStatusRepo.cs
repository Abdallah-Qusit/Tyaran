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
    public class DeliveryStatusRepo :IDeliveryStatusRepo
    {
        private readonly TyaranDbContext _context;

        public DeliveryStatusRepo(TyaranDbContext context)
        {
            _context = context;
        }
        public async Task ToggleStatusAsync(int deliveryManId,bool isOnline)
        {
            await _context.Database.ExecuteSqlRawAsync(
                @"EXEC DeliveryMan_ToggleStatus
                @DeliveryManID,
                @IsOnline",
                new SqlParameter("@DeliveryManID", deliveryManId),
                new SqlParameter("@IsOnline", isOnline)
            );
        }
        public async Task<List<Order>> GetAvailableOrdersAsync()
        {
            return await _context.Orders
            .Where(o => o.DriverId == null && o.OrderStatus == "Pending")
            .Include(o => o.Restaurant)
            .Include(o => o.Address)
            .ToListAsync();
        }

        public async Task AcceptOrderAsync(int orderId, int deliveryManId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                @"EXEC DeliveryMan_AcceptOrder
                @OrderID,
                @DeliveryManID",
                new SqlParameter("@OrderID", orderId),
                new SqlParameter("@DeliveryManID", deliveryManId)
            );
        }
        public async Task<decimal> GetCurrentSessionEarningsAsync(int deliveryManId)
        {
            var result= await _context.Database
                .SqlQueryRaw<decimal>(
                    @"EXEC DeliveryMan_CurrentSessionEarnings
                    @DeliveryManID",
                    new SqlParameter(
                        "@DeliveryManID",
                        deliveryManId)
                ).ToListAsync();
            return result.FirstOrDefault();
        }
        public async Task<decimal> GetTotalEarningsAsync(int deliveryManId)
        {
            var result = await _context.Database
        .SqlQueryRaw<decimal>(
            @"EXEC DeliveryMan_TotalEarnings
              @DeliveryManID",
            new SqlParameter(
                "@DeliveryManID",
                deliveryManId)
        )
        .ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return;

            order.OrderStatus = status;
            await _context.SaveChangesAsync();
        }
        public async Task<Order?> GetOrderAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Restaurant)
                    .ThenInclude(r => r.Address)
                .Include(o => o.Restaurant)
                    .ThenInclude(r => r.RestaurantPhones)
                .Include(o => o.Address)
                .Include(o => o.User)
                    .ThenInclude(u => u.UserPhones)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}
