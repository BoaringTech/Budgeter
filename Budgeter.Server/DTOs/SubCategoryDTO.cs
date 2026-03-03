namespace Budgeter.Server.DTOs
{
    public record SubCategoryDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required CategoryDTO Cateogry { get; set; }
    }
}
