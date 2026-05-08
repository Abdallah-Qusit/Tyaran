using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Services.Abstraction;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.PL.Controllers;

public class RestaurantsController : Controller
{
    private readonly IRestaurantService _service;
    private readonly IWebHostEnvironment _environment;

    public RestaurantsController(
        IRestaurantService service,
        IWebHostEnvironment environment)
    {
        _service = service;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var restaurants = await _service.GetAllAsync();
        return View(restaurants);
    }

    public async Task<IActionResult> Details(int id)
    {
        var restaurant = await _service.GetByIdAsync(id);

        if (restaurant == null)
            return NotFound();

        return View(restaurant);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        Restaurant restaurant,
        IFormFile logoFile)
    {
        if (logoFile != null && logoFile.Length > 0)
        {
            var folder = Path.Combine(
                _environment.WebRootPath,
                "images",
                "restaurants");

            Directory.CreateDirectory(folder);

            var fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(logoFile.FileName);

            var filePath =
                Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await logoFile.CopyToAsync(stream);
            }

            restaurant.LogoUrl =
                "/images/restaurants/" + fileName;
        }

        if (ModelState.IsValid)
        {
            await _service.CreateAsync(restaurant);
            return RedirectToAction(nameof(Index));
        }

        return View(restaurant);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var restaurant = await _service.GetByIdAsync(id);

        if (restaurant == null)
            return NotFound();

        return View(restaurant);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        Restaurant restaurant,
        IFormFile logoFile)
    {
        if (id != restaurant.RestaurantId)
            return NotFound();

        if (logoFile != null && logoFile.Length > 0)
        {
            var folder = Path.Combine(
                _environment.WebRootPath,
                "images",
                "restaurants");

            Directory.CreateDirectory(folder);

            var fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(logoFile.FileName);

            var filePath =
                Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await logoFile.CopyToAsync(stream);
            }

            restaurant.LogoUrl =
                "/images/restaurants/" + fileName;
        }

        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(restaurant);
            return RedirectToAction(nameof(Index));
        }

        return View(restaurant);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var restaurant = await _service.GetByIdAsync(id);

        if (restaurant == null)
            return NotFound();

        return View(restaurant);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}