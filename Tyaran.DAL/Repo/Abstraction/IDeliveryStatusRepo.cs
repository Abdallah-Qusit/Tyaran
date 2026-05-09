using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repo.Abstraction
{
    public interface IDeliveryStatusRepo
    {
        Task ToggleStatusAsync(int deliveryManId,bool isOnline);
        Task<List<Order>> GetAvailableOrdersAsync();
        Task AcceptOrderAsync(int orderId,int deliveryManId);
        Task<decimal> GetCurrentSessionEarningsAsync(int deliveryManId);
        Task<decimal> GetTotalEarningsAsync(int deliveryManId);
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task<Order?> GetOrderAsync(int orderId);
    }
}
