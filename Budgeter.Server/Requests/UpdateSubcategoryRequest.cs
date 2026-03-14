namespace Budgeter.Server.Requests
{
    public class UpdateSubcategoryRequest
    {
        public string? Category {  get; set; }
        public string? Name { get; set; }
        public int? Order { get; set; }
    }
}
