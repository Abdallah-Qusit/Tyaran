using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.BLL.ModelVM.Admin;
using Tyaran.BLL.Service.Abstraction;
using Tyaran.DAL.Repo.Abstraction;

namespace Tyaran.BLL.Service.Implementation
{
    public class AdminService : IAdminService
    {

        private readonly IAdminRepository _repo;

        public AdminService(IAdminRepository repo)
        {
            _repo = repo;
        }

        public async Task<AdminDashboardVm> GetDashboardAsync()
        {
            return new AdminDashboardVm
            {
                UsersCount = await _repo.GetUsersCountAsync(),
                DeliveriesCount = await _repo.GetDeliveriesCountAsync(),
                RestaurantsCount = await _repo.GetRestaurantsCountAsync(),
                OrdersCount = await _repo.GetOrdersCountAsync()
            };
        }

        public async Task<PendingRequestsVm> GetPendingRequestsAsync()
        {
            return new PendingRequestsVm
            {
                Deliveries = await _repo.GetPendingDeliveriesAsync(),
                Restaurants = await _repo.GetPendingRestaurantsAsync()
            };
        }

        public Task ApproveDeliveryAsync(int id) =>
                _repo.ApproveDeliveryAsync(id);

        public Task RejectDeliveryAsync(int id) =>
            _repo.RejectDeliveryAsync(id);

        public Task ApproveRestaurantAsync(int id) =>
            _repo.ApproveRestaurantAsync(id);

        public Task RejectRestaurantAsync(int id) =>
            _repo.RejectRestaurantAsync(id);


    }
}
