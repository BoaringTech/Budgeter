namespace Budgeter.Server.Entities
{
    public class Subcategory
    {
        public int Id { get; set; }
        public required Category Category { get; set; }
        public required string Name { get; set; }
        public required int Order { get; set; }
    }
}
