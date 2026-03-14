using Budgeter.Server.DTOs;
using Budgeter.Server.Entities;
using Budgeter.Server.Repositories.Interfaces;
using Budgeter.Server.Requests;
using Budgeter.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Budgeter.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountService accountService, IAccountRepository accountRepository)
        {
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        // GET /accounts/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAccounts()
        {
            IEnumerable<Account> users = await _accountRepository.GetAllAccountsAsync();
            IEnumerable<AccountDTO> usersDTO = users.Select(_accountService.TranslateAccount);
            return Ok(usersDTO);
        }

        // POST /accounts/
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> CreateAccount([FromBody] CreateAccountRequest request)
        {
            try
            {
                Account user = await _accountRepository.CreateAccountAsync(request);
                AccountDTO userDTO = _accountService.TranslateAccount(user);
                return CreatedAtAction(nameof(CreateAccount), new { id = userDTO.Id }, userDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /accounts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, UpdateAccountRequest request)
        {
            Account? user = await _accountRepository.UpdateAccountAsync(id, request);

            if (user == null)
                return NotFound();

            AccountDTO userDTO = _accountService.TranslateAccount(user);

            return Ok(userDTO);
        }

        // DELETE /accounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            bool deleted = await _accountRepository.DeleteAccountAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
