using Budgeter.Server.Entities;
using Budgeter.Server.Requests;

namespace Budgeter.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // CREATE
        Task<User> CreateUserAsync(CreateUserRequest request);

        // READ
        Task<IEnumerable<User>> GetAllUsersAsync();

        // UPDATE
        Task<User?> UpdateUserAsync(int id, UpdateUserRequest request);

        // DELETE
        Task<bool> DeleteUserAsync(int id);

        Task<User?> GetUserAsync(string username);
    }
}
