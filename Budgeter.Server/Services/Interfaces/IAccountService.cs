using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;

namespace Budgeter.Server.Services.Interfaces
{
    public interface IAccountService
    {
        AccountDTO TranslateAccount(Account c);
    }
}
