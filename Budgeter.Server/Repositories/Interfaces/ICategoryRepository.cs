using Budgeter.Server.DTOs;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        // CREATE
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryRequest request);

        // READ
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();

        // UPDATE
        Task<CategoryDTO?> UpdateCategoryAsync(int id, UpdateCategoryRequest request);

        // DELETE
        Task<bool> DeleteCategoryAsync(int id);
    }
}
