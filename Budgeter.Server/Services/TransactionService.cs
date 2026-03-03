using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Budgeter.Server.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BudgeterDbContext _context;
        private readonly ILogger<TransactionService> _logger;

        public TransactionService(BudgeterDbContext context, ILogger<TransactionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionsAsync()
        {
            return await _context.Transactions
                .OrderByDescending(t => t.DateTime)
                .Select(ToTransactionDTO())
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
                .Select(t => new TransactionModel
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Description = t.Description,
                    Date = t.Date,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category != null ? t.Category.Name : "Uncategorized",
                    Type = t.Type
                })
                .ToListAsync();
        }

        public async Task<TransactionDTO> CreateTransactionAsync(CreateTransactionRequest request)
        {
            var transaction = new Transaction
            {
                Amount = request.Amount,
                Description = request.Description,
                Date = request.Date,
                CategoryId = request.CategoryId,
                Type = request.Type,
                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(transaction)
                .Reference(t => t.Category)
                .LoadAsync();

            return new TransactionModel
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Category?.Name ?? "Uncategorized",
                Type = transaction.Type
            };
        }

        public async Task<TransactionDTO?> UpdateTransactionAsync(int id, UpdateTransactionRequest request)
        {
            var transaction = await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return null;

            // Update only provided fields
            if (request.Amount.HasValue)
                transaction.Amount = request.Amount.Value;

            if (request.Description != null)
                transaction.Description = request.Description;

            if (request.Date.HasValue)
                transaction.Date = request.Date.Value;

            if (request.CategoryId.HasValue)
                transaction.CategoryId = request.CategoryId.Value;

            if (request.Type.HasValue)
                transaction.Type = request.Type.Value;

            transaction.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new TransactionModel
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Description = transaction.Description,
                Date = transaction.Date,
                CategoryId = transaction.CategoryId,
                CategoryName = transaction.Category?.Name ?? "Uncategorized",
                Type = transaction.Type
            };
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

        public async Task<decimal> GetTotalSpendingByCategoryAsync(int categoryId, DateTime start, DateTime end)
        {
            return await _context.Transactions
                .Where(t => t.CategoryId == categoryId
                    && t.Type == TransactionType.Expense
                    && t.Date >= start
                    && t.Date <= end)
                .SumAsync(t => t.Amount);
        }

        private static Expression<Func<Transaction, TransactionDTO>> ToTransactionDTO()
        {
            return t => TranslateTransaction(t);
        }

        private static TransactionDTO TranslateTransaction(Transaction t)
        {
            return new TransactionDTO
            {
                Id = t.Id,
                User = t.User != null ? t.User.Name : string.Empty,
                DateTime = t.DateTime,
                Account = t.Account != null ? t.Account.Name : string.Empty,
                TransactionType = t.TransactionType,
                Category = t.Category != null ? t.Category.Name : string.Empty,
                SubCategory = t.SubCategory != null ? t.SubCategory.Name : string.Empty,
                Amount = t.Amount,
                Bookmarked = t.Bookmarked,
                Notes = t.Notes
            };
        }
    }
}
