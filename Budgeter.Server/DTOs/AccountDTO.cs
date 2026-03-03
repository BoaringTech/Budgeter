namespace Budgeter.Server.DTOs
{
    public record AccountDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
