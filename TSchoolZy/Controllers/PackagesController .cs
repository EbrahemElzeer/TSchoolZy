using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSchoolZy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _svc;

        public PackagesController(IPackageService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pkg = await _svc.GetByIdAsync(id);
            return pkg == null ? NotFound() : Ok(pkg);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PackageDto dto)
        {
            dto.IsUpdate = false;
            var result = await _svc.AddAsync(dto);
            if (!result) return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PackageDto dto)
        {
            dto.IsUpdate = true;
            if (dto.Id != 0 && dto.Id != id) return BadRequest("ID mismatch");

            var updated = await _svc.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _svc.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
