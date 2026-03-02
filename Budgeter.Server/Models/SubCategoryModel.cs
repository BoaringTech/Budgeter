namespace Budgeter.Server.Models
{
    public record SubCategoryModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public CategoryModel? Cateogry { get; set; }
    }
}
