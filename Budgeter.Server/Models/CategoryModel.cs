namespace Budgeter.Server.Models
{
    public record CategoryModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
