using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        // GET /users/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            IEnumerable<User> users = await _userRepository.GetAllUsersAsync();
            IEnumerable<UserDTO> usersDTO = users.Select(_userService.TranslateUser);
            return Ok(usersDTO);
        }

        // POST /users/
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                User user = await _userRepository.CreateUserAsync(request);
                UserDTO userDTO = _userService.TranslateUser(user);
                return CreatedAtAction(nameof(CreateUser), new { id = userDTO.Id }, userDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            User? user = await _userRepository.UpdateUserAsync(id, request);

            if (user == null)
                return NotFound();

            UserDTO userDTO = _userService.TranslateUser(user);

            return Ok(userDTO);
        }

        // DELETE /users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool deleted = await _userRepository.DeleteUserAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
