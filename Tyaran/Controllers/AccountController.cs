using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.ModelVM.ViewModels;
using Tyaran.BLL.Service.Abstraction;

namespace Tyaran.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // ---------------- LOGIN ----------------

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return result.Data!.Role switch
            {
                "Admin" => RedirectToAction("Index4", "Home"),
                "User" => RedirectToAction("Index", "Home"),
                "Delivery" => RedirectToAction("Index2", "Home"),
                "RestaurantOwner" => RedirectToAction("Index3", "Home"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // ---------------- LOGOUT ----------------

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }

        // ---------------- REGISTER USER ----------------

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _authService.RegisterUserAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Login");
        }

        // ---------------- REGISTER DELIVERY ----------------

        [HttpGet]
        public IActionResult RegisterDelivery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDelivery(RegisterDeliveryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.RegisterDeliveryAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Login");
        }

        // ---------------- REGISTER RESTAURANT ----------------

        [HttpGet]
        public IActionResult RegisterRestaurant()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterRestaurant(RegisterRestaurantViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.RegisterRestaurantAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            return RedirectToAction("Login");
        }

        // ---------------- CHANGE PASSWORD ----------------

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.ChangePasswordAsync(model);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }

            ViewBag.Success = result.Message;
            return View();
        }
    }
}