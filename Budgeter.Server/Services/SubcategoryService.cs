using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Services.Interfaces;

namespace Budgeter.Server.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        public SubcategoryDTO TranslateSubcategory(Subcategory s)
        {
            return new SubcategoryDTO
            {
                Id = s.Id,
                Category = s.Category.Name,
                Name = s.Name,
                Order = s.Order,
            };
        }
    }
}
