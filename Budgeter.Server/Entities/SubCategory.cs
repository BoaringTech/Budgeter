namespace Budgeter.Server.Entities
{
    public class Subcategory
    {
        public int Id { get; set; }

        public int CategoryId { get; set; } = -1;

        public Category? Category { get; set; }

        public required string Name { get; set; }
        public required int Order { get; set; }
        public bool IsSystem { get; set; } = false;
    }
}
