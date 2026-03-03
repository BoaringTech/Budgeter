using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Entities
{
    public class BudgeterDbContext : DbContext
    {
        public BudgeterDbContext(DbContextOptions<BudgeterDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<BudgetSetting> BudgetSettings { get; set; }
        public DbSet<Category> Categoies { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
