using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.BLL.ModelVM.Admin;

namespace Tyaran.BLL.Service.Abstraction
{
    public interface IAdminService
    {
        Task<AdminDashboardVm> GetDashboardAsync();
        Task<PendingRequestsVm> GetPendingRequestsAsync();

        Task ApproveDeliveryAsync(int id);
        Task RejectDeliveryAsync(int id);
        Task ApproveRestaurantAsync(int id);
        Task RejectRestaurantAsync(int id);

    }
}
