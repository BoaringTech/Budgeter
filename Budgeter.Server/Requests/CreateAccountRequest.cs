namespace Budgeter.Server.Requests
{
    public class CreateAccountRequest
    {
        public required string Name { get; set; }
        public required int Order { get; set; }
    }
}
