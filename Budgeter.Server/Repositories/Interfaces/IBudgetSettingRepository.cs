using Budgeter.Server.Entities;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface IBudgetSettingRepository
    {
        // CREATE
        Task<BudgetSetting> CreateBudgetSettingAsync(CreateBudgetSettingRequest request);

        // READ
        Task<IEnumerable<BudgetSetting>> GetAllBudgetSettingsAsync();

        // UPDATE
        Task<BudgetSetting?> UpdateBudgetSettingAsync(int id, UpdateBudgetSettingRequest request);

        // DELETE
        Task<bool> DeleteBudgetSettingAsync(int id);
    }
}
