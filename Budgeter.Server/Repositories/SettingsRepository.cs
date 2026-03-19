using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly ILogger<SettingsRepository> _logger;

        public SettingsRepository(BudgeterDbContext context,
            ILogger<SettingsRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<IEnumerable<BooleanSetting>> GetAllBooleanSettingsAsync()
        {
            return await _context.BooleanSettings
                .ToListAsync();
        }

        public async Task<BooleanSetting?> UpdateBooleanSettingAsync(int id, UpdateBooleanSettingRequest request)
        {
            BooleanSetting? setting = await _context.BooleanSettings
                .FirstOrDefaultAsync(c => c.Id == id);

            if (setting == null)
                return null;

            // Update only provided fields
            if (request.Enabled != null)
                setting.Enabled = (bool)request.Enabled;

            await _context.SaveChangesAsync();

            return setting;
        }
    }
}
