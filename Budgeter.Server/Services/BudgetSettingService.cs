using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Services.Interfaces;

namespace Budgeter.Server.Services
{
    public class BudgetSettingService : IBudgetSettingService
    {
        public BudgetSettingDTO TranslateBudgetSetting(BudgetSetting b)
        {
            return new BudgetSettingDTO
            {
                Id = b.Id,
                Category = b.Category.Name,
                Amount = b.Amount,
                Order = b.Order,
            };
        }
    }
}
