using Budgeter.Server.Entities;
using Budgeter.Server.Enums;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(BudgeterDbContext context,
            ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryRequest request)
        {
            Category category = CreateCategoryObjectAsync(request);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(category)
                .Reference(c => c.Name)
                .LoadAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Order)
                .OrderBy(c => c.TransactionType)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);

            if (category == null)
                return null;

            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, UpdateCategoryRequest request)
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

            return category;
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

        public async Task<Category?> GetCategoryAsync(string categoryName)
        {
            return await _context.Categories.Where(a => a.Name == categoryName).FirstOrDefaultAsync();
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
