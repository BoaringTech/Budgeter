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
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingsService;
        private readonly ISettingsRepository _settingsRepository;

        public SettingsController(ISettingService settingsService, ISettingsRepository settingsRepository)
        {
            _settingsService = settingsService;
            _settingsRepository = settingsRepository;
        }

        // GET /settings/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooleanSettingDTO>>> GetSettings()
        {
            IEnumerable<BooleanSetting> users = await _settingsRepository.GetAllBooleanSettingsAsync();
            IEnumerable<BooleanSettingDTO> usersDTO = users.Select(_settingsService.TranslateSetting);
            return Ok(usersDTO);
        }

        // PUT /settings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSetting(int id, UpdateBooleanSettingRequest request)
        {
            BooleanSetting? user = await _settingsRepository.UpdateBooleanSettingAsync(id, request);

            if (user == null)
                return NotFound();

            BooleanSettingDTO userDTO = _settingsService.TranslateSetting(user);

            return Ok(userDTO);
        }
    }
}
