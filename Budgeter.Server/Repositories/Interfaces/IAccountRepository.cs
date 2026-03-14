using Budgeter.Server.Entities;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        // CREATE
        Task<Account> CreateAccountAsync(CreateAccountRequest request);

        // READ
        Task<IEnumerable<Account>> GetAllAccountsAsync();

        // UPDATE
        Task<Account?> UpdateAccountAsync(int id, UpdateAccountRequest request);

        // DELETE
        Task<bool> DeleteAccountAsync(int id);
    }
}
