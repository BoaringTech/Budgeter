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
    public class SubcategoriesController : ControllerBase
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly ISubcategoryRepository _subcategoryRepository;

        public SubcategoriesController(ISubcategoryService subcategoryService, ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryService = subcategoryService;
            _subcategoryRepository = subcategoryRepository;
        }

        // GET /subcategories/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubcategoryDTO>>> GetSubcategorys()
        {
            IEnumerable<Subcategory> users = await _subcategoryRepository.GetAllSubcategoriesAsync();
            IEnumerable<SubcategoryDTO> usersDTO = users.Select(_subcategoryService.TranslateSubcategory);
            return Ok(usersDTO);
        }

        // POST /subcategories/
        [HttpPost]
        public async Task<ActionResult<SubcategoryDTO>> CreateSubcategory([FromBody] CreateSubcategoryRequest request)
        {
            try
            {
                Subcategory user = await _subcategoryRepository.CreateSubcategoryAsync(request);
                SubcategoryDTO userDTO = _subcategoryService.TranslateSubcategory(user);
                return CreatedAtAction(nameof(CreateSubcategory), new { id = userDTO.Id }, userDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /subcategories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubcategory(int id, UpdateSubcategoryRequest request)
        {
            Subcategory? user = await _subcategoryRepository.UpdateSubcategoryAsync(id, request);

            if (user == null)
                return NotFound();

            SubcategoryDTO userDTO = _subcategoryService.TranslateSubcategory(user);

            return Ok(userDTO);
        }

        // DELETE /subcategories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            bool deleted = await _subcategoryRepository.DeleteSubcategoryAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
