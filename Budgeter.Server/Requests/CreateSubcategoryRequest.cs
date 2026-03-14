namespace Budgeter.Server.Requests
{
    public class CreateSubcategoryRequest
    {
        public required string Category { get; set; }
        public required string Name { get; set; }
        public required int Order { get; set; }
    }
}
