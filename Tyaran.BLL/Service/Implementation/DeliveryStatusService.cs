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
    public class DeliveryStatusService : IDeliveryStatusService
    {
        private readonly IDeliveryStatusRepo _repo;
        public DeliveryStatusService(IDeliveryStatusRepo repo)
        {
            _repo = repo;
        }
        public async Task ToggleStatusAsync(int deliveryManId,bool isOnline)
        {
            await _repo.ToggleStatusAsync(deliveryManId,isOnline);
        }
        public async Task<List<Order>> GetAvailableOrdersAsync()
        {
            return await _repo.GetAvailableOrdersAsync();
        }
        public async Task AcceptOrderAsync(int orderId,int deliveryManId)
        {
            await _repo.AcceptOrderAsync(orderId,deliveryManId);
        }
        public async Task<decimal> GetCurrentSessionEarningsAsync(int deliveryManId)
        {
            return await _repo.GetCurrentSessionEarningsAsync(deliveryManId);
        }
        public async Task<decimal> GetTotalEarningsAsync(int deliveryManId)
        {
            return await _repo.GetTotalEarningsAsync(deliveryManId);
        }
        public Task UpdateStatusAsync(int orderId, string status)
        => _repo.UpdateOrderStatusAsync(orderId, status);
        public Task<Order?> GetOrderDetailsAsync(int orderId)
        => _repo.GetOrderAsync(orderId);
    }
}
