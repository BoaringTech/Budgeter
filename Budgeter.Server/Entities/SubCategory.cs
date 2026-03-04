namespace Budgeter.Server.Entities
{
    public class Subcategory
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required Category Cateogry { get; set; }
    }
}
