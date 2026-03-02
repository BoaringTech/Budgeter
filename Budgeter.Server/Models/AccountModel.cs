namespace Budgeter.Server.Models
{
    public record AccountModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
