using BodegaHogar.Domain.Entities;

namespace BodegaHogar.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> AddAsync(Category category);
    }
}