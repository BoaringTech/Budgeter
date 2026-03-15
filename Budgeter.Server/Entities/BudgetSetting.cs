using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Entities
{
    public class BudgetSetting
    {
        public int Id { get; set; }


        public int CategoryId { get; set; } = -1;

        public Category? Category { get; set; }

        [Precision(11, 2)]
        public required decimal Amount { get; set; }
        public required int Order { get; set; }
        public bool IsSystem { get; set; } = false;
    }
}
