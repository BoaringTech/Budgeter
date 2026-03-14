using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;

namespace Budgeter.Server.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO TranslateUser(User u);
    }
}
