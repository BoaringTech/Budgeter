using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Enums;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(BudgeterDbContext context, ILogger<TransactionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .OrderByDescending(t => t.Date)
                .Select(t => TranslateTransaction(t))
                .ToListAsync();
        }

        public async Task<TransactionDTO?> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return null;

            return TranslateTransaction(transaction);
        }

        public async Task<IEnumerable<TransactionDTO>> GetTransactionsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.Date >= start && t.Date <= end)
                .OrderBy(t => t.Date)
                .Select(t => TranslateTransaction(t))
                .ToListAsync();
        }

        public async Task<TransactionDTO> CreateTransactionAsync(CreateTransactionRequest request)
        {
            Transaction transaction = await CreateTransactionObjectAsync(request);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(transaction)
                .Reference(t => t.Category)
                .LoadAsync();

            return TranslateTransaction(transaction);
        }

        public async Task<TransactionDTO?> UpdateTransactionAsync(int id, UpdateTransactionRequest request)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return null;

            // Update only provided fields
            if (request.UserName != null)
                transaction.User = await GetUserAsync(request.UserName);

            if (request.DateTime != null)
                transaction.Date = (DateTime)request.DateTime;

            if(request.AccountName != null)
                transaction.Account = await GetAccountAsync(request.AccountName);

            if(request.TransactionType != null)
                transaction.TransactionType = GetTransactionType(request.TransactionType);

            if (request.CategoryName != null)
                transaction.Category = await GetCategoryAsync(request.CategoryName);

            if (request.SubcategoryName != null)
                transaction.SubCategory = await GetSubcategoryAsync(request.SubcategoryName);

            if (request.Amount.HasValue)
                transaction.Amount = request.Amount.Value;

            if (request.Bookmarked != null)
                transaction.Bookmarked = (bool)request.Bookmarked;

            if (request.Notes != null)
                transaction.Notes = request.Notes;

            await _context.SaveChangesAsync();

            return TranslateTransaction(transaction);
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
                return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalSpendingByCategoryAsync(string categoryName, DateTime start, DateTime end)
        {
            return await _context.Transactions
                .Where(t => (t.Category != null ? t.Category.Name : string.Empty) == categoryName
                    && t.TransactionType == TransactionTypes.Expense
                    && t.Date >= start
                    && t.Date <= end)
                .SumAsync(t => t.Amount);
        }

        private static TransactionDTO TranslateTransaction(Transaction t)
        {
            return new TransactionDTO
            {
                Id = t.Id,
                User = t.User != null ? t.User.Name : string.Empty,
                DateTime = t.Date,
                Account = t.Account != null ? t.Account.Name : string.Empty,
                TransactionType = t.TransactionType,
                Category = t.Category != null ? t.Category.Name : string.Empty,
                SubCategory = t.SubCategory != null ? t.SubCategory.Name : string.Empty,
                Amount = t.Amount,
                Bookmarked = t.Bookmarked,
                Notes = t.Notes
            };
        }

        private async Task<User?> GetUserAsync(string username)
        {
            return await _context.Users.Where(u => u.Name == username).FirstOrDefaultAsync();
        }

        private async Task<Account?> GetAccountAsync(string accountName)
        {
            return await _context.Accounts.Where(a => a.Name == accountName).FirstOrDefaultAsync();
        }

        private async Task<Category?> GetCategoryAsync(string categoryName)
        {
            return await _context.Categories.Where(a => a.Name == categoryName).FirstOrDefaultAsync();
        }

        private async Task<Subcategory?> GetSubcategoryAsync(string subcategoryName)
        {
            return await _context.Subcategories.Where(a => a.Name == subcategoryName).FirstOrDefaultAsync();
        }

        private TransactionTypes GetTransactionType(string transactionType)
        {
            if (transactionType is null)
            {
                throw new ArgumentNullException(nameof(transactionType) + " cannot be null.");
            }

            if (Enum.TryParse(transactionType, out TransactionTypes type))
            {
                return type;
            }

            throw new ArgumentException(nameof(transactionType) + " is not a valid value. Value: " + transactionType);
        }

        private async Task<Transaction> CreateTransactionObjectAsync(CreateTransactionRequest request)
        {
            Transaction transaction = new Transaction
            {
                Date = request.DateTime,
                TransactionType = GetTransactionType(request.TransactionType),
                Amount = request.Amount,
                Bookmarked = request.Bookmarked,
                Notes = request.Notes
            };

            if(!string.IsNullOrEmpty(request.UserName))
            {
                transaction.User = await GetUserAsync(request.UserName);
            }

            if (!string.IsNullOrEmpty(request.AccountName)) 
            {
                transaction.Account = await GetAccountAsync(request.AccountName);
            }

            if(!string.IsNullOrEmpty(request.CategoryName))
            {
                transaction.Category = await GetCategoryAsync(request.CategoryName);

                if(!string.IsNullOrEmpty(request.SubCategoryName))
                {
                    transaction.SubCategory = await GetSubcategoryAsync(request.SubCategoryName);
                }
            }

            return transaction;
        }

    }
}
