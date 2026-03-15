using Budgeter.Server.Enums;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; } = -1;
        public int AccountId { get; set; } = -1;
        public int CategoryId { get; set; } = -1;
        public int? SubcategoryId { get; set; }

        public User? User { get; set; }
        public Account? Account { get; set; }
        public Category? Category { get; set; }
        public Subcategory? SubCategory { get; set; }

        public required DateTime Date { get; set; }
        public required TransactionTypes TransactionType { get; set; }
        [Precision(11, 2)]
        public required decimal Amount { get; set; }
        public string? Merchant { get; set; }
        public required bool Bookmarked { get; set; }
        public string? Notes { get; set; }
    }
}
