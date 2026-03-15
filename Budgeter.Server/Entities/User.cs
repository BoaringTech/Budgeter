namespace Budgeter.Server.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Order { get; set; }
        public bool IsSystem { get; set; } = false;
    }
}
