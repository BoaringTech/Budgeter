using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.DTOs
{
    public class BudgetSettingDTO
    {
        public int Id { get; set; }
        public required CategoryDTO Category { get; set; }
        [Precision(11, 2)]
        public required decimal Amount { get; set; }
    }
}
