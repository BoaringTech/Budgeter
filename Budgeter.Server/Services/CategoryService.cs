using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Services.Interfaces;

namespace Budgeter.Server.Services
{
    public class CategoryService : ICategoryService
    {
        public CategoryDTO TranslateCategory(Category c)
        {
            return new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                TransactionType = c.TransactionType,
                Order = c.Order,
            };
        }
    }
}
