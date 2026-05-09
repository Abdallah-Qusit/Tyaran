using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Database;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repo.Abstraction
{
    public interface IAdminRepository
    {
        Task<int> GetUsersCountAsync();
        Task<int> GetDeliveriesCountAsync();
        Task<int> GetRestaurantsCountAsync();
        Task<int> GetOrdersCountAsync();

        Task<List<(DeliveryMan Delivery, User User)>> GetPendingDeliveriesAsync();
        Task<List<Restaurant>> GetPendingRestaurantsAsync();


        Task ApproveDeliveryAsync(int deliveryId);
        Task RejectDeliveryAsync(int deliveryId);
        Task ApproveRestaurantAsync(int restaurantId);
        Task RejectRestaurantAsync(int restaurantId);


    }
}
