using Azure.Core;
using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Budgeter.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BudgeterDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(BudgeterDbContext context,
            ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            User user = CreateUserObjectAsync(request);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Load the category for the response
            await _context.Entry(user)
                .Reference(u => u.Name)
                .LoadAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .OrderByDescending(u => u.Order)
                .ToListAsync();
        }

        public async Task<User?> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            User? user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return null;

            // Update only provided fields
            if (request.Name != null)
                user.Name = request.Name;

            if (request.Order != null)
                user.Order = (int)request.Order;

            await _context.SaveChangesAsync();

            return user;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private User CreateUserObjectAsync(CreateUserRequest request)
        {
            User user = new User
            {
                Name = request.Name,
                Order = request.Order
            };

            return user;
        }
    }
}
