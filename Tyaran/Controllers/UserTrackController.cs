using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Service.Abstraction;

namespace Tyaran.PL.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class UserTrackController : Controller
    {
        private readonly IUserTrackService _service;
        public UserTrackController(IUserTrackService service)
        {
            _service = service;
        }
        [HttpGet("{orderId}/track")]
        public async Task<IActionResult> TrackOrder(int orderId)
        {
            var order =await _service.TrackOrderAsync(orderId);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
