using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Requests
{
    /// <summary>
    /// Class to take in a Creation request from the user.
    /// No field is required.
    /// If all fields are null, this should not have been created.
    /// </summary>
    public class UpdateTransactionRequest
    {
        public string? UserName { get; set; }
        public DateTime? DateTime { get; set; }
        public string? AccountName { get; set; }
        public string? TransactionType { get; set; }
        public string? CategoryName { get; set; }
        public string? SubcategoryName { get; set; }
        [Precision(11, 2)]
        public decimal? Amount { get; set; }
        public bool? Bookmarked { get; set; }
        public string? Notes { get; set; }
    }
}
