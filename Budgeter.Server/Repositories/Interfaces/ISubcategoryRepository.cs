using Budgeter.Server.DTOs;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface ISubcategoryRepository
    {
        // CREATE
        Task<SubcategoryDTO> CreateSubcategoryAsync(CreateSubcategoryRequest request);

        // READ
        Task<IEnumerable<SubcategoryDTO>> GetAllSubcategoriesAsync();

        // UPDATE
        Task<SubcategoryDTO?> UpdateSubcategoryAsync(int id, UpdateSubcategoryRequest request);

        // DELETE
        Task<bool> DeleteSubcategoryAsync(int id);
    }
}
