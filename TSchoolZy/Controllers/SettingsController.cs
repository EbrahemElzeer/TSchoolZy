using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSchoolZy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _svc;

        public SettingsController(ISettingService svc) => _svc = svc;

        // GET: api/Settings
        [HttpGet]
        public async Task<ActionResult<SettingDto>> Get()
        {
            var setting = await _svc.GetAsync();
            return setting == null ? NotFound() : Ok(setting);
        }

        // PUT: api/Settings
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SettingDto dto)
        {
            dto.IsUpdate = true;
            var ok = await _svc.UpdateAsync(dto);
            return ok ? NoContent() : NotFound();
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SettingDto>> Create([FromBody] SettingDto dto)
        {
            dto.IsUpdate = false;
            var existed = await _svc.GetAsync();
            if (existed != null)
                return BadRequest("Settings already exist.");

            var created = await _svc.CreateAsync(dto);
            if (!created)
                return BadRequest("Failed to create settings.");

            return CreatedAtAction(nameof(Get), new { }, dto);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var existed = await _svc.GetAsync();
            if (existed == null)
                return NotFound("No settings to delete.");

            var ok = await _svc.DeleteAsync();  
            return ok ? NoContent() : BadRequest("Failed to delete settings.");
        }
    }
}
