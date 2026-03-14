using Budgeter.Server.Enums;

namespace Budgeter.Server.Requests
{
    public class UpdateCategoryRequest
    {
        public string? Name { get; set; }
        public string? TransactionType { get; set; }
        public int? Order { get; set; }
    }
}
