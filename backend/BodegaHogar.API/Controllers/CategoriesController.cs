using BodegaHogar.Application.Interfaces;
using BodegaHogar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BodegaHogar.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _repository.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCategory = await _repository.AddAsync(category);
            return StatusCode(StatusCodes.Status201Created, createdCategory);
        }
    }
}