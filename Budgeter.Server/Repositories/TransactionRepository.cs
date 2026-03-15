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
        private readonly IAccountRepository _accountRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(BudgeterDbContext context,
            IAccountRepository accountRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            ISubcategoryRepository subcategoryRepository,
            ILogger<TransactionRepository> logger)
        {
            _context = context;
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _subcategoryRepository = subcategoryRepository;
            _logger = logger;
            
        }

        public async Task<Transaction> CreateTransactionAsync(CreateTransactionRequest request)
        {
            Transaction transaction = await CreateTransactionObjectAsync(request);

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return null;

            return transaction;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.Date >= start && t.Date <= end)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

        public async Task<Transaction?> UpdateTransactionAsync(int id, UpdateTransactionRequest request)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return null;

            // Update only provided fields
            if (request.UserName != null)
                transaction.User = await _userRepository.GetUserAsync(request.UserName);

            if (request.DateTime != null)
                transaction.Date = (DateTime)request.DateTime;

            if(request.AccountName != null)
                transaction.Account = await _accountRepository.GetAccountAsync(request.AccountName);

            if(request.TransactionType != null)
                transaction.TransactionType = GetTransactionType(request.TransactionType);

            if (request.CategoryName != null)
                transaction.Category = await _categoryRepository.GetCategoryAsync(request.CategoryName);

            if (request.SubcategoryName != null)
                transaction.SubCategory = await _subcategoryRepository.GetSubcategoryAsync(request.SubcategoryName);

            if (request.Amount.HasValue)
                transaction.Amount = request.Amount.Value;

            if (request.Bookmarked != null)
                transaction.Bookmarked = (bool)request.Bookmarked;

            if (request.Notes != null)
                transaction.Notes = request.Notes;

            await _context.SaveChangesAsync();

            return transaction;
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
                var user = await _userRepository.GetUserAsync(request.UserName);
                if(user != null)
                    transaction.UserId = user.Id;
            }

            if (!string.IsNullOrEmpty(request.AccountName)) 
            {
                var account = await _accountRepository.GetAccountAsync(request.AccountName);
                if(account != null)
                    transaction.AccountId = account.Id;
            }

            if(!string.IsNullOrEmpty(request.CategoryName))
            {
                 var category = await _categoryRepository.GetCategoryAsync(request.CategoryName);

                if(category != null)
                {
                    transaction.CategoryId = category.Id;

                    if (!string.IsNullOrEmpty(request.SubCategoryName))
                    {
                        var subcategory = await _subcategoryRepository.GetSubcategoryAsync(request.SubCategoryName);
                        if(subcategory != null)
                            transaction.SubcategoryId = subcategory.Id; 
                    }
                }
            }

            return transaction;
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
    }
}
