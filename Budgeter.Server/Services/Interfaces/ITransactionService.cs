using Budgeter.Server.DTOs;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Services.Interfaces
{
    public interface ITransactionService
    {
        // CREATE
        Task<TransactionDTO> CreateTransactionAsync(CreateTransactionRequest request);

        // READ
        Task<TransactionDTO?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAsync(DateTime start, DateTime end);
        Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync();

        // UPDATE
        Task<TransactionDTO?> UpdateTransactionAsync(int id, UpdateTransactionRequest request);

        // DELETE
        Task<bool> DeleteTransactionAsync(int id);
    }
}
