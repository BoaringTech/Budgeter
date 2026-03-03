namespace Budgeter.Server.DTOs
{
    public record CategoryDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
