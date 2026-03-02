namespace Budgeter.Server.Models
{
    public class BudgetSettingModel
    {
        public int Id { get; set; }
        public required CategoryModel Category { get; set; }
        public required Decimal Amount { get; set; }
    }
}
