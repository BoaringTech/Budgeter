using Budgeter.Server.Entities;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface ISubcategoryRepository
    {
        // CREATE
        Task<Subcategory> CreateSubcategoryAsync(CreateSubcategoryRequest request);

        // READ
        Task<IEnumerable<Subcategory>> GetAllSubcategoriesAsync();

        // UPDATE
        Task<Subcategory?> UpdateSubcategoryAsync(int id, UpdateSubcategoryRequest request);

        // DELETE
        Task<bool> DeleteSubcategoryAsync(int id);
    }
}
