using BodegaHogar.Application.Interfaces;
using BodegaHogar.Domain.Entities;
using BodegaHogar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BodegaHogar.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // Retorna todos los productos de Supabase
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            // Aseguramos fechas e IDs automáticos antes de guardar
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(); // Dispara el INSERT en PostgreSQL

            return product;
        }
    }
}