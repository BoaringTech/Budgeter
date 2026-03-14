using Budgeter.Server.DTOs;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // CREATE
        Task<UserDTO> CreateUserAsync(CreateUserRequest request);

        // READ
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        // UPDATE
        Task<UserDTO?> UpdateUserAsync(int id, UpdateUserRequest request);

        // DELETE
        Task<bool> DeleteUserAsync(int id);
    }
}
