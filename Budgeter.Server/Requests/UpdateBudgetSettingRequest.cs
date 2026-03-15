namespace Budgeter.Server.Requests
{
    public class UpdateBudgetSettingRequest
    {
        public string? Category { get; set; }
        public decimal? Amount { get; set; }
        public int? Order { get; set; }
    }
}
