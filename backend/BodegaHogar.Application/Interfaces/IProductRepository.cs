using BodegaHogar.Domain.Entities;

namespace BodegaHogar.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
    }
}