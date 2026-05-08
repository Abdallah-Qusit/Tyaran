using Microsoft.AspNetCore.Http;
using Tyaran.BLL.Services.Abstraction;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repositories.Abstraction;

namespace Tyaran.BLL.Services.Implementation;

public class MenuCategoryService : IMenuCategoryService
{
    private readonly IMenuCategoryRepository _repository;

    public MenuCategoryService(
        IMenuCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MenuCategory>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MenuCategory?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(MenuCategory menuCategory)
    {
        await _repository.AddAsync(menuCategory);
    }

    public async Task UpdateAsync(MenuCategory menuCategory)
    {
        await _repository.UpdateAsync(menuCategory);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _repository.ExistsAsync(id);
    }
    private async Task<string> SaveFileAsync(IFormFile file, string folder)
    {
        var uploadsPath = Path.Combine("wwwroot", "uploads", folder);
        Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var fullPath = Path.Combine(uploadsPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/uploads/{folder}/{fileName}";
    }
}