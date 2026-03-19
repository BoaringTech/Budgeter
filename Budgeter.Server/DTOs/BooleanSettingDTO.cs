namespace Budgeter.Server.DTOs
{
    public class BooleanSettingDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required bool Enabled { get; set; }
    }
}
