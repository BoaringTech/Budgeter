using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Services.Interfaces;

namespace Budgeter.Server.Services
{
    public class AccountService : IAccountService
    {
        public AccountDTO TranslateAccount(Account c)
        {
            return new AccountDTO
            {
                Id = c.Id,
                Name = c.Name,
                Order = c.Order,
                IsSystem = c.IsSystem
            };
        }
    }
}
