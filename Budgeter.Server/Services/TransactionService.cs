using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Services.Interfaces;


namespace Budgeter.Server.Services
{
    public class TransactionService : ITransactionService
    {
        public TransactionDTO TranslateTransaction(Transaction t)
        {
            return new TransactionDTO
            {
                Id = t.Id,
                User = t.User != null ? t.User.Name : string.Empty,
                DateTime = t.Date,
                Account = t.Account != null ? t.Account.Name : string.Empty,
                TransactionType = t.TransactionType.ToString(),
                Category = t.Category != null ? t.Category.Name : string.Empty,
                SubCategory = t.SubCategory != null ? t.SubCategory.Name : string.Empty,
                Amount = t.Amount,
                Merchant = t.Merchant,
                Bookmarked = t.Bookmarked,
                Notes = t.Notes
            };
        }
    }
}
