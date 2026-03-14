using Budgeter.Server.Enums;

namespace Budgeter.Server.DTOs
{
    public record CategoryDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required TransactionTypes TransactionType { get; set; }
        public required int Order { get; set; }
    }
}
