using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Budgeter.Server.Requests
{
    /// <summary>
    /// Class to take in a Creation request from the user.
    /// Only required properties: transaction type, amount, bookmarked.
    /// </summary>
    public class CreateTransactionRequest
    {
        public string? UserName { get; set; }
        public required DateTime DateTime { get; set; }
        public string?  AccountName { get; set; }
        [Required]
        public required string TransactionType { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        [Required]
        [Precision(11, 2)]
        public required decimal Amount { get; set; }
        public required bool Bookmarked { get; set; }
        public string? Notes { get; set; }
    }
}
