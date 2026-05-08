using Microsoft.AspNetCore.Http;
using Tyaran.BLL.Services.Abstraction;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repositories.Abstraction;

namespace Tyaran.BLL.Services.Implementation;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository;

    public MenuItemService(
        IMenuItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MenuItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(MenuItem menuItem)
    {
        await _repository.AddAsync(menuItem);
    }

    public async Task UpdateAsync(MenuItem menuItem)
    {
        await _repository.UpdateAsync(menuItem);
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