using Budgeter.Server.Enums;

namespace Budgeter.Server.Models
{
    public record TransactionModel
    {
        public required int Id { get; set; }
        public required UserModel User { get; set; }
        public required AccountModel Account { get; set; }
        public required TransactionTypes TransactionType { get; set; }
        public required CategoryModel Category { get; set; }
        public SubCategoryModel? SubCategory { get; set; }
        public required Decimal Amount { get; set; }
        public required bool Bookmarked { get; set; }
        public string? Notes { get; set; }
    }
}
