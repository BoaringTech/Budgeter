using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        // CREATE
        Task<Category> CreateCategoryAsync(CreateCategoryRequest request);

        // READ
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByNameAsync(string name);

        // UPDATE
        Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryRequest request);

        // DELETE
        Task<bool> DeleteCategoryAsync(int id);

        Task<Category?> GetCategoryAsync(string categoryName);
    }
}
