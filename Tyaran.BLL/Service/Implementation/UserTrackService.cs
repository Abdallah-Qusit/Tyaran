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
    public class UserTrackService : IUserTrackService
    {
        private readonly IUserTrackRepo _repo;
        public UserTrackService(IUserTrackRepo repo)
        {
            _repo = repo;
        }
        public async Task<Order?> TrackOrderAsync(int orderId)
        {
            return await _repo.TrackOrderAsync(orderId);
        }
    }
}
