using Budgeter.Server.Enums;

namespace Budgeter.Server.Requests
{
    public class CreateCategoryRequest
    {
        public required string Name { get; set; }
        public required string TransactionType { get; set; }
        public required int Order { get; set; }
    }
}
