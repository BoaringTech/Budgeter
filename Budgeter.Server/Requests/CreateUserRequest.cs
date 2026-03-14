namespace Budgeter.Server.Requests
{
    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required int Order { get; set; }
    }
}
