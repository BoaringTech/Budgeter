using Budgeter.Server.Entities;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        // READ
        Task<IEnumerable<BooleanSetting>> GetAllBooleanSettingsAsync();

        // UPDATE
        Task<BooleanSetting?> UpdateBooleanSettingAsync(int id, UpdateBooleanSettingRequest request);
    }
}
