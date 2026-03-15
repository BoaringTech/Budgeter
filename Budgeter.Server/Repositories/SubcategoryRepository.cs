using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<SubcategoryRepository> _logger;

        public SubcategoryRepository(BudgeterDbContext context,
            ICategoryRepository categoryRepository,
            ILogger<SubcategoryRepository> logger)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _logger = logger;

        }
        public async Task<Subcategory> CreateSubcategoryAsync(CreateSubcategoryRequest request)
        {
            Subcategory subcategory = await CreateSubcategoryObjectAsync(request);

            _context.Subcategories.Add(subcategory);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(subcategory)
                .Reference(c => c.Name)
                .LoadAsync();

            return subcategory;
        }

        public async Task<IEnumerable<Subcategory>> GetAllSubcategoriesAsync()
        {
            return await _context.Subcategories
                .OrderBy(s => s.Order)
                .OrderBy(s => s.Category)
                .ToListAsync();
        }

        public async Task<Subcategory?> UpdateSubcategoryAsync(int id, UpdateSubcategoryRequest request)
        {
            Subcategory? subcategory = await _context.Subcategories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (subcategory == null)
                return null;

            // Update only provided fields
            if (request.Category != null)
            {
                var category = await _categoryRepository.GetCategoryByNameAsync(request.Category);

                if (category is null)
                {
                    throw new NullReferenceException($"{request.Category} is not a valid category. " +
                        $"Cannot create subcategory");
                }

                subcategory.Category = category;
            }

            if (request.Name != null)
                subcategory.Name = request.Name;

            if (request.Order != null)
                subcategory.Order = (int)request.Order;

            await _context.SaveChangesAsync();

            return subcategory;
        }

        public async Task<bool> DeleteSubcategoryAsync(int id)
        {
            var subcategory = await _context.Subcategories.FindAsync(id);
            if (subcategory == null)
                return false;

            _context.Subcategories.Remove(subcategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Subcategory?> GetSubcategoryAsync(string subcategoryName)
        {
            return await _context.Subcategories.Where(a => a.Name == subcategoryName).FirstOrDefaultAsync();
        }

        private async Task<Subcategory> CreateSubcategoryObjectAsync(CreateSubcategoryRequest request)
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(request.Category);

            if (category is null)
            {
                throw new NullReferenceException($"{request.Category} is not a valid category. " +
                    $"Cannot create subcategory");
            }

            Subcategory subcategory = new Subcategory
            {
                CategoryId = category.Id,
                Name = request.Name,
                Order = request.Order
            };

            return subcategory;
        }
    }
}
