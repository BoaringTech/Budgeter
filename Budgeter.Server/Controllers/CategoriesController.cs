using Budgeter.Server.DTOs;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryService;

        public CategoriesController(ICategoryRepository categoryService)
        {
            _categoryService = categoryService;
        }

        // GET /categories/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            IEnumerable<CategoryDTO> categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // POST /transactions
        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            try
            {
                CategoryDTO category = await _categoryService.CreateCategoryAsync(request);
                return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /transactions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request)
        {
            CategoryDTO? category = await _categoryService.UpdateCategoryAsync(id, request);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // DELETE /transactions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool deleted = await _categoryService.DeleteCategoryAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
