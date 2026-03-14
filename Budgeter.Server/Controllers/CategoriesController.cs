using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryService categoryService, ICategoryRepository categoryRepository)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }

        // GET /categories/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            IEnumerable<CategoryDTO> categoriesDTO = categories.Select(_categoryService.TranslateCategory);
            return Ok(categoriesDTO);
        }

        // POST /categories/
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            try
            {
                Category category = await _categoryRepository.CreateCategoryAsync(request);
                CategoryDTO categoryDTO = _categoryService.TranslateCategory(category);
                return CreatedAtAction(nameof(CreateCategory), new { id = categoryDTO.Id }, categoryDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request)
        {
            Category? category = await _categoryRepository.UpdateCategoryAsync(id, request);

            if (category == null)
                return NotFound();

            CategoryDTO categoryDTO = _categoryService.TranslateCategory(category);

            return Ok(categoryDTO);
        }

        // DELETE /categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool deleted = await _categoryRepository.DeleteCategoryAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
