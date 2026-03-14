using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;

namespace Budgeter.Server.Services.Interfaces
{
    public interface ISubcategoryService
    {
        SubcategoryDTO TranslateSubcategory(Subcategory s);
    }
}
