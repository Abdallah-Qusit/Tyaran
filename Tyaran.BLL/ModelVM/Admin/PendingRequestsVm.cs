using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.ModelVM.Admin
{
    public class PendingRequestsVm
    {
        public List<(DeliveryMan Delivery, User User)> Deliveries { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
