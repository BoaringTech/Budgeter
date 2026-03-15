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
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(BudgeterDbContext context,
            IAccountService accountService,
            ILogger<AccountRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Account> CreateAccountAsync(CreateAccountRequest request)
        {
            Account account = CreateAccountObjectAsync(request);

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(account)
                .Reference(u => u.Name)
                .LoadAsync();

            return account;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts
                .OrderBy(u => u.Order)
                .ToListAsync();
        }

        public async Task<Account?> UpdateAccountAsync(int id, UpdateAccountRequest request)
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

            return account;
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
