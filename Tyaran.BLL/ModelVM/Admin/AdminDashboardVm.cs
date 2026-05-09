using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyaran.BLL.ModelVM.Admin
{
    public class AdminDashboardVm
    {
        public int UsersCount { get; set; }
        public int OrdersCount { get; set; }
        public int DeliveriesCount { get; set; }
        public int RestaurantsCount { get; set; }

    }
}
