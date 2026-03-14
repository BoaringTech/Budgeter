using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Enums;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(BudgeterDbContext context,
            ICategoryService categoryService,
            ILogger<CategoryRepository> logger)
        {
            _context = context;
            _categoryService = categoryService;
            _logger = logger;

        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryRequest request)
        {
            Category category = CreateCategoryObjectAsync(request);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(category)
                .Reference(c => c.Name)
                .LoadAsync();

            return _categoryService.TranslateCategory(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .OrderByDescending(c => c.Order)
                .OrderByDescending(c => c.TransactionType)
                .Select(c => _categoryService.TranslateCategory(c))
                .ToListAsync();
        }

        public async Task<CategoryDTO?> UpdateCategoryAsync(int id, UpdateCategoryRequest request)
        {
            Category? category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return null;

            // Update only provided fields
            if (request.Name != null)
                category.Name = request.Name;

            if (request.TransactionType != null)
                category.TransactionType = GetTransactionType(request.TransactionType);

            if (request.Order != null)
                category.Order = (int)request.Order;

            await _context.SaveChangesAsync();

            return _categoryService.TranslateCategory(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }


        private Category CreateCategoryObjectAsync(CreateCategoryRequest request)
        {
            Category category = new Category
            {
                Name = request.Name,
                TransactionType = GetTransactionType(request.TransactionType),
                Order = request.Order
            };

            return category;
        }

        private TransactionTypes GetTransactionType(string transactionType)
        {
            if (transactionType is null)
            {
                throw new ArgumentNullException(nameof(transactionType) + " cannot be null.");
            }

            if (Enum.TryParse(transactionType, out TransactionTypes type))
            {
                return type;
            }

            throw new ArgumentException(nameof(transactionType) + " is not a valid value. Value: " + transactionType);
        }
    }
}
