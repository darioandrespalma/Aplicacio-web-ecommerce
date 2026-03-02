using BodegaHogar.Application.Interfaces;
using BodegaHogar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BodegaHogar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        // Inyectamos la interfaz, no la base de datos directa
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProduct = await _repository.AddAsync(product);

            return StatusCode(StatusCodes.Status201Created, createdProduct);
        }
    }
}