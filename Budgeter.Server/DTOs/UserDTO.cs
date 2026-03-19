namespace Budgeter.Server.DTOs
{
    public class UserDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int Order { get; set; }
        public required bool IsSystem { get; set; }
    }
}
