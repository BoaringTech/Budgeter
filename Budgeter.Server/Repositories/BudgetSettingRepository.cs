using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class BudgetSettingRepository : IBudgetSettingRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<BudgetSettingRepository> _logger;

        public BudgetSettingRepository(BudgeterDbContext context,
            ICategoryRepository categoryRepository,
            ILogger<BudgetSettingRepository> logger)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _logger = logger;

        }

        public async Task<BudgetSetting> CreateBudgetSettingAsync(CreateBudgetSettingRequest request)
        {
            BudgetSetting budgetSetting = await CreateBudgetSettingObjectAsync(request);

            _context.BudgetSettings.Add(budgetSetting);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(budgetSetting)
                .Reference(c => c.Category)
                .LoadAsync();

            return budgetSetting;
        }

        public async Task<IEnumerable<BudgetSetting>> GetAllBudgetSettingsAsync()
        {
            return await _context.BudgetSettings
                .OrderBy(s => s.Order)
                .OrderBy(s => s.Category)
                .ToListAsync();
        }

        public async Task<BudgetSetting?> UpdateBudgetSettingAsync(int id, UpdateBudgetSettingRequest request)
        {
            BudgetSetting? budgetSetting = await _context.BudgetSettings
                .FirstOrDefaultAsync(c => c.Id == id);

            if (budgetSetting == null)
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

                budgetSetting.Category = category;
            }

            if (request.Amount != null)
                budgetSetting.Amount = (decimal)request.Amount;

            if (request.Order != null)
                budgetSetting.Order = (int)request.Order;

            await _context.SaveChangesAsync();

            return budgetSetting;
        }

        public async Task<bool> DeleteBudgetSettingAsync(int id)
        {
            var budgetSetting = await _context.BudgetSettings.FindAsync(id);
            if (budgetSetting == null)
                return false;

            _context.BudgetSettings.Remove(budgetSetting);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<BudgetSetting> CreateBudgetSettingObjectAsync(CreateBudgetSettingRequest request)
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(request.Category);

            if (category is null)
            {
                throw new NullReferenceException($"{request.Category} is not a valid category. " +
                    $"Cannot create subcategory");
            }


            BudgetSetting budgetSetting = new BudgetSetting
            {
                Category = category,
                Amount = request.Amount,
                Order = request.Order
            };

            return budgetSetting;
        }
    }
}
