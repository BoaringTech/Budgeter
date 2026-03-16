using Budgeter.Server.Enums;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.DTOs
{
    public class TransactionDTO
    {
        public required int Id { get; set; }
        public string? User { get; set; }
        public required DateTime DateTime { get; set; }
        public string? Account { get; set; }
        public required string TransactionType { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        [Precision(11,2)]
        public required decimal Amount { get; set; }
        public string? Merchant { get; set; }
        public required bool Bookmarked { get; set; }
        public string? Notes { get; set; }
    }
}
