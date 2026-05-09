using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Service.Abstraction;

namespace Tyaran.PL.Controllers
{
    public class DeliveryStatusController : Controller
    {
        private readonly IDeliveryStatusService _service;

        public DeliveryStatusController(IDeliveryStatusService service)
        {
            _service = service;
        }

        // DASHBOARD PAGE
        [HttpGet]
        public IActionResult Dashboard(int deliveryManId)
        {
            ViewBag.DeliveryManId = deliveryManId;
            return View();
        }

        // TOGGLE STATUS
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int deliveryManId, bool isOnline)
        {
            await _service.ToggleStatusAsync(deliveryManId, isOnline);

            return RedirectToAction("Dashboard", new { deliveryManId });
        }

        // AVAILABLE ORDERS PAGE
        [HttpGet]
        public async Task<IActionResult> AvailableOrders(int deliveryManId)
        {
            var orders = await _service.GetAvailableOrdersAsync();

            ViewBag.DeliveryManId = deliveryManId;

            return View(orders);
        }

        // ACCEPT ORDER
        [HttpPost]
        public async Task<IActionResult> AcceptOrder(int deliveryManId, int orderId)
        {
            await _service.AcceptOrderAsync(orderId, deliveryManId);

            return RedirectToAction(
        "Track",
        new
        {
            orderId = orderId
        });
        }

        // SESSION EARNINGS PAGE
        [HttpGet]
        public async Task<IActionResult> SessionEarnings(int deliveryManId)
        {
            var earnings = await _service.GetCurrentSessionEarningsAsync(deliveryManId);

            ViewBag.DeliveryManId = deliveryManId;

            return View(earnings);
        }

        // TOTAL EARNINGS PAGE
        [HttpGet]
        public async Task<IActionResult> TotalEarnings(int deliveryManId)
        {
            var earnings = await _service.GetTotalEarningsAsync(deliveryManId);

            ViewBag.DeliveryManId = deliveryManId;

            return View(earnings);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            await _service.UpdateStatusAsync(orderId, status);
            return Ok();
        }
        public async Task<IActionResult> Track(int orderId)
        {
            var order = await _service.GetOrderDetailsAsync(orderId);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
