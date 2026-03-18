using Budgeter.Server.Entities;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        // CREATE
        Task<Transaction> CreateTransactionAsync(CreateTransactionRequest request);

        // READ
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime start, DateTime end);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<IEnumerable<Transaction>> GetAllBookmarkedTransactionsAsync();

        // UPDATE
        Task<Transaction?> UpdateTransactionAsync(int id, UpdateTransactionRequest request);

        // DELETE
        Task<bool> DeleteTransactionAsync(int id);
    }
}
