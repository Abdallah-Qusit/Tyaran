using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repo.Abstraction
{
    public interface IUserTrackRepo
    {
        Task<Order?> TrackOrderAsync(int orderId);
    }
}
