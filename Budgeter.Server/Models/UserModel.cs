namespace Budgeter.Server.Models
{
    public record UserModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
