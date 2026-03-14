using Budgeter.Server.DTOs;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userService;

        public UsersController(IUserRepository userService)
        {
            _userService = userService;
        }

        // GET /transactions/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            IEnumerable<UserDTO> users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // POST /transactions
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                UserDTO user = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /transactions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            UserDTO? user = await _userService.UpdateUserAsync(id, request);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // DELETE /transactions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool deleted = await _userService.DeleteUserAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
