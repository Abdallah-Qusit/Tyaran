using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Service.Abstraction;

namespace Tyaran.PL.Controllers
{
    [ApiController]
    [Route("api/user/profile")]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService _service;

        public UserProfileController(IUserProfileService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            var user =await _service.GetProfileAsync(userId);
            if (user == null)
                return NotFound();
            return View(user);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateProfile(int userId,string firstName,string lastName,string email)
        {
            await _service.UpdateProfileAsync(userId,firstName,lastName,email);
            return View("Profile Updated");
        }
    }
}
