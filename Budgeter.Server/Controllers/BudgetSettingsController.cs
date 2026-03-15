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
    public class BudgetSettingsController : ControllerBase
    {
        private readonly IBudgetSettingService _budgetSettingService;
        private readonly IBudgetSettingRepository _budgetSettingRepository;

        public BudgetSettingsController(IBudgetSettingService budgetSettingService, 
            IBudgetSettingRepository budgetSettingRepository)
        {
            _budgetSettingService = budgetSettingService;
            _budgetSettingRepository = budgetSettingRepository;
        }

        // GET /budgetSettings/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetSettingDTO>>> GetBudgetSettings()
        {
            IEnumerable<BudgetSetting> users = await _budgetSettingRepository.GetAllBudgetSettingsAsync();
            IEnumerable<BudgetSettingDTO> usersDTO = users.Select(_budgetSettingService.TranslateBudgetSetting);
            return Ok(usersDTO);
        }

        // POST /budgetSettings/
        [HttpPost]
        public async Task<ActionResult<BudgetSettingDTO>> CreateBudgetSetting([FromBody] CreateBudgetSettingRequest request)
        {
            try
            {
                BudgetSetting user = await _budgetSettingRepository.CreateBudgetSettingAsync(request);
                BudgetSettingDTO userDTO = _budgetSettingService.TranslateBudgetSetting(user);
                return CreatedAtAction(nameof(CreateBudgetSetting), new { id = userDTO.Id }, userDTO);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while creating the transaction");
            }
        }

        // PUT /budgetSettings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudgetSetting(int id, UpdateBudgetSettingRequest request)
        {
            BudgetSetting? user = await _budgetSettingRepository.UpdateBudgetSettingAsync(id, request);

            if (user == null)
                return NotFound();

            BudgetSettingDTO userDTO = _budgetSettingService.TranslateBudgetSetting(user);

            return Ok(userDTO);
        }

        // DELETE /budgetSettings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudgetSetting(int id)
        {
            bool deleted = await _budgetSettingRepository.DeleteBudgetSettingAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
