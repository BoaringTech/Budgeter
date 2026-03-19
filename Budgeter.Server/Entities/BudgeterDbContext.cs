using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Entities
{
    public class BudgeterDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BudgetSetting> BudgetSettings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BooleanSetting> BooleanSettings { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        
        public BudgeterDbContext(DbContextOptions<BudgeterDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Transaction relationships
            modelBuilder.Entity<Transaction>(entity =>
            {
                // User relationship - required
                entity.HasOne(t => t.User)
                    .WithMany()
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(); // Enforces required in database

                // Account relationship - required
                entity.HasOne(t => t.Account)
                    .WithMany()
                    .HasForeignKey(t => t.AccountId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Category relationship - required
                entity.HasOne(t => t.Category)
                    .WithMany()
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // SubCategory relationship - optional
                entity.HasOne(t => t.SubCategory)
                    .WithMany()
                    .HasForeignKey(t => t.SubcategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                // Set database defaults (optional since we have C# defaults)
                entity.Property(t => t.UserId).HasDefaultValue(-1);
                entity.Property(t => t.AccountId).HasDefaultValue(-1);
                entity.Property(t => t.CategoryId).HasDefaultValue(-1);
            });

            // Configure BudgetSetting relationship
            modelBuilder.Entity<BudgetSetting>(entity =>
            {
                entity.HasOne(b => b.Category)
                    .WithMany()
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.Property(b => b.CategoryId).HasDefaultValue(-1);
            });

            // Configure Subcategory relationship
            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.HasOne(b => b.Category)
                    .WithMany()
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                entity.Property(b => b.CategoryId).HasDefaultValue(-1);
            });


            // Create default values in database
            modelBuilder.Entity<Account>().HasData(
                new Account 
                {
                    Id = -1,
                    Name = "No Account",
                    Order = -1,
                    IsSystem = true
                }    
            );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = -1,
                    Name = "Uncategorized",
                    TransactionType = Enums.TransactionTypes.Expense,
                    Order = -1,
                    IsSystem = true
                }, 
                new Category
                {
                    Id = -2,
                    Name = "Uncategorized",
                    TransactionType = Enums.TransactionTypes.Income,
                    Order = -1,
                    IsSystem = true
                }
            );

            modelBuilder.Entity<BudgetSetting>().HasData(
                new BudgetSetting
                {
                    Id = -1,
                    CategoryId = -1,
                    Amount = 0,
                    Order = -1,
                    IsSystem = true
                }
            );

            modelBuilder.Entity<BooleanSetting>().HasData(
                new BooleanSetting
                {
                    Id = 1,
                    Name = "TrackAccounts",
                    Enabled = true,
                },
                new BooleanSetting
                {
                    Id = 2,
                    Name = "TrackUsers",
                    Enabled = true,
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = -1,
                    Name = "No User",
                    Order = -1,
                    IsSystem = true
                }
            );
        }
    }
}
