using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Service.Abstraction;

namespace Tyaran.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService dashboardService)
        {
            _adminService = dashboardService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var vm = await _adminService.GetDashboardAsync();
            return View(vm);
        }

        public async Task<IActionResult> Requests()
        {
            var vm = await _adminService.GetPendingRequestsAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveDelivery(int id)
        {
            await _adminService.ApproveDeliveryAsync(id);
            return RedirectToAction(nameof(Requests));
        }

        [HttpPost]
        public async Task<IActionResult> RejectDelivery(int id)
        {
            await _adminService.RejectDeliveryAsync(id);
            return RedirectToAction(nameof(Requests));
        }

        [HttpPost]
        public async Task<IActionResult> ApproveRestaurant(int id)
        {
            await _adminService.ApproveRestaurantAsync(id);
            return RedirectToAction(nameof(Requests));
        }

        [HttpPost]
        public async Task<IActionResult> RejectRestaurant(int id)
        {
            await _adminService.RejectRestaurantAsync(id);
            return RedirectToAction(nameof(Requests));
        }


    }
}
