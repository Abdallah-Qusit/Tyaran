using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Tyaran.BLL.Service.Abstraction;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.PL.Controllers
{
    [Route("home")]
    public class UserHomeController : Controller
    {
        private readonly IUserHomeService _userHomeService;

        public UserHomeController(IUserHomeService userHomeService)
        {
            _userHomeService = userHomeService;
        }
        [HttpGet("most-ordered")]
        public async Task<IActionResult> MostOrdered()
        {
            var result = await _userHomeService.GetMostOrderedAsync();
            return View(result);
        }

        [HttpGet("most-visited")]
        public async Task<IActionResult> MostVisited()
        {
            var result = await _userHomeService.GetMostVisitedAsync();
            return Ok(result);
        }

        [HttpGet("recommended/{userId}")]
        public async Task<IActionResult> Recommended(int userId)
        {
            var result = await _userHomeService.GetRecommendedAsync(userId);
            return Ok(result);
        }

    }
}
