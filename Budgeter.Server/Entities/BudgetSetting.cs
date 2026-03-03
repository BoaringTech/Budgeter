using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Entities
{
    public class BudgetSetting
    {
        public int Id { get; set; }
        public required Category Category { get; set; }
        [Precision(11, 2)]
        public required decimal Amount { get; set; }
    }
}
