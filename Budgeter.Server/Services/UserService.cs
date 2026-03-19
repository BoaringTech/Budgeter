using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Services.Interfaces;

namespace Budgeter.Server.Services
{
    public class UserService : IUserService
    {
        public UserDTO TranslateUser(User u)
        {
            return new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Order = u.Order,
                IsSystem = u.IsSystem
            };
        }
    }
}
