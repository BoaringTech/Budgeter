using Budgeter.Server.DTOs;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        // CREATE
        Task<AccountDTO> CreateAccountAsync(CreateAccountRequest request);

        // READ
        Task<IEnumerable<AccountDTO>> GetAllAccountsAsync();

        // UPDATE
        Task<AccountDTO?> UpdateAccountAsync(int id, UpdateAccountRequest request);

        // DELETE
        Task<bool> DeleteAccountAsync(int id);
    }
}
