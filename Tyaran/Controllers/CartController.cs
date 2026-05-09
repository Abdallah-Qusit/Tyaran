using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Service.Abstraction;

namespace Tyaran.PL.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly IUserCartService _service;
        public CartController(IUserCartService service)
        {
            _service = service;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddItem(int userId, int itemId, int quantity, string specialInst)
        {
            await _service.AddItemAsync(
                userId,
                itemId,
                quantity,
                specialInst
            );
            return View("Item Added");
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart =
                await _service.GetCartAsync(userId);
            return View(cart);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem(int cartItemId, string specialInst, int quantity)
        {
            await _service.UpdateItemAsync(
                cartItemId,
                specialInst,
                quantity
            );

            return View("Updated");
        }
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteItem(int cartItemId)
        {
            await _service.RemoveItemAsync(cartItemId);

            return View("Deleted");
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(int userId, int addressId, string paymentMethod)
        {
            int orderId =
                await _service.CheckoutAsync(
                    userId,
                    addressId,
                    paymentMethod
                );
            return View(new
            {
                Message = "Order Created",
                OrderId = orderId
            });
        }
    }
}
