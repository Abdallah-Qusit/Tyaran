using Microsoft.AspNetCore.Mvc;
using Tyaran.BLL.Services.Abstraction;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.PL.Controllers;

public class MenuCategoriesController : Controller
{
    private readonly IMenuCategoryService _service;

    public MenuCategoriesController(
        IMenuCategoryService service)
    {
        _service = service;
    }

    // =========================
    // INDEX
    // =========================

    public async Task<IActionResult> Index()
    {
        var categories =
            await _service.GetAllAsync();

        return View(categories);
    }

    // =========================
    // DETAILS
    // =========================

    public async Task<IActionResult> Details(int id)
    {
        var menuCategory =
            await _service.GetByIdAsync(id);

        if (menuCategory == null)
        {
            return NotFound();
        }

        return View(menuCategory);
    }

    // =========================
    // CREATE
    // =========================

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        MenuCategory menuCategory)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(menuCategory);

            return RedirectToAction(nameof(Index));
        }

        return View(menuCategory);
    }

    // =========================
    // EDIT
    // =========================

    public async Task<IActionResult> Edit(int id)
    {
        var menuCategory =
            await _service.GetByIdAsync(id);

        if (menuCategory == null)
        {
            return NotFound();
        }

        return View(menuCategory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        MenuCategory menuCategory)
    {
        if (id != menuCategory.MenuCatId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(menuCategory);

            return RedirectToAction(nameof(Index));
        }

        return View(menuCategory);
    }

    // =========================
    // DELETE
    // =========================

    public async Task<IActionResult> Delete(int id)
    {
        var menuCategory =
            await _service.GetByIdAsync(id);

        if (menuCategory == null)
        {
            return NotFound();
        }

        return View(menuCategory);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}