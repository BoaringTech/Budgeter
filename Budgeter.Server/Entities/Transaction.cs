using Budgeter.Server.Enums;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Entities
{
    public class Transaction
    {
        public required int Id { get; set; }
        public User? User { get; set; }
        public required DateTime DateTime { get; set; }
        public Account? Account { get; set; }
        public required TransactionTypes TransactionType { get; set; }
        public Category? Category { get; set; }
        public SubCategory? SubCategory { get; set; }
        [Precision(11, 2)]
        public required decimal Amount { get; set; }
        public required bool Bookmarked { get; set; }
        public string? Notes { get; set; }
    }
}
