using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.Service.Abstraction
{
    public interface IDeliveryStatusService
    {
        Task ToggleStatusAsync(int deliveryManId,bool isOnline);
        Task<List<Order>> GetAvailableOrdersAsync();
        Task AcceptOrderAsync(int orderId,int deliveryManId);
        Task<decimal> GetCurrentSessionEarningsAsync(int deliveryManId);
        Task<decimal> GetTotalEarningsAsync(int deliveryManId);
        Task UpdateStatusAsync(int orderId, string status);
        Task<Order?> GetOrderDetailsAsync(int orderId);
    }
}
