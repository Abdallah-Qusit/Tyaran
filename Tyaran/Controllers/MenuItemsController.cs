using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Services.Abstraction;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.PL.Controllers;

public class MenuItemsController : Controller
{
    private readonly IMenuItemService _service;
    private readonly IWebHostEnvironment _environment;

    public MenuItemsController(
        IMenuItemService service,
        IWebHostEnvironment environment)
    {
        _service = service;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        var menuItems = await _service.GetAllAsync();
        return View(menuItems);
    }

    public async Task<IActionResult> Details(int id)
    {
        var menuItem = await _service.GetByIdAsync(id);

        if (menuItem == null)
            return NotFound();

        return View(menuItem);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        MenuItem menuItem,
        IFormFile imageFile)
    {
        if (imageFile != null && imageFile.Length > 0)
        {
            var folder = Path.Combine(
                _environment.WebRootPath,
                "images",
                "menuitems");

            Directory.CreateDirectory(folder);

            var fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(imageFile.FileName);

            var filePath =
                Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            menuItem.ImageUrl =
                "/images/menuitems/" + fileName;
        }

        if (ModelState.IsValid)
        {
            await _service.CreateAsync(menuItem);
            return RedirectToAction(nameof(Index));
        }

        return View(menuItem);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var menuItem = await _service.GetByIdAsync(id);

        if (menuItem == null)
            return NotFound();

        return View(menuItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        MenuItem menuItem,
        IFormFile imageFile)
    {
        if (id != menuItem.ItemId)
            return NotFound();

        if (imageFile != null && imageFile.Length > 0)
        {
            var folder = Path.Combine(
                _environment.WebRootPath,
                "images",
                "menuitems");

            Directory.CreateDirectory(folder);

            var fileName =
                Guid.NewGuid().ToString() +
                Path.GetExtension(imageFile.FileName);

            var filePath =
                Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            menuItem.ImageUrl =
                "/images/menuitems/" + fileName;
        }

        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(menuItem);
            return RedirectToAction(nameof(Index));
        }

        return View(menuItem);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var menuItem = await _service.GetByIdAsync(id);

        if (menuItem == null)
            return NotFound();

        return View(menuItem);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}