namespace Budgeter.Server.Entities
{
    public class BooleanSetting
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required bool Enabled { get; set; }
    }
}
