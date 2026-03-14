using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(BudgeterDbContext context,
            IAccountService accountService,
            ILogger<AccountRepository> logger)
        {
            _context = context;
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<AccountDTO> CreateAccountAsync(CreateAccountRequest request)
        {
            Account account = CreateAccountObjectAsync(request);

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(account)
                .Reference(u => u.Name)
                .LoadAsync();

            return _accountService.TranslateAccount(account);
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .OrderByDescending(u => u.Order)
                .Select(u => _accountService.TranslateAccount(u))
                .ToListAsync();
        }

        public async Task<AccountDTO?> UpdateAccountAsync(int id, UpdateAccountRequest request)
        {
            Account? account = await _context.Accounts
                .FirstOrDefaultAsync(u => u.Id == id);

            if (account == null)
                return null;

            // Update only provided fields
            if (request.Name != null)
                account.Name = request.Name;

            if (request.Order != null)
                account.Order = (int)request.Order;

            await _context.SaveChangesAsync();

            return _accountService.TranslateAccount(account);
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
                return false;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        private Account CreateAccountObjectAsync(CreateAccountRequest request)
        {
            Account account = new Account
            {
                Name = request.Name,
                Order = request.Order
            };

            return account;
        }
    }
}
