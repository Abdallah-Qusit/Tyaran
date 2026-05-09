using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Service.Abstraction;

namespace Tyaran.PL.Controllers
{
    [ApiController]
    [Route("restaurants")]
    public class MenuController : Controller
    {
        private readonly IUserMenuService _menuService;
        public MenuController(IUserMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet("{restaurantId}/menu")]
        public async Task<IActionResult>GetRestaurantMenu(int restaurantId)
        {
            var menu = await _menuService.GetRestaurantMenuAsync(restaurantId);
            return View(menu);
        }
    }
}
