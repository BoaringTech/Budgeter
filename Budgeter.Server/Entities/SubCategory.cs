namespace Budgeter.Server.Entities
{
    public class SubCategory
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required Category Cateogry { get; set; }
    }
}
