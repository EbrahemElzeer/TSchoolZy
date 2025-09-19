using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSchoolZy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {

        private readonly IContactFormService _svc;

      
        public ContactFormController(IContactFormService svc) => _svc = svc;
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _svc.GetAllAsync();
            return Ok(list);
        }
        [Authorize]
        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            var dto = await _svc.GetByIdAsync(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactFormDto dto)
        {
            dto.IsUpdate = false;
            var ok = await _svc.AddAsync(dto);
            if (!ok) return BadRequest("Unable to submit form.");
            Console.WriteLine(await new StreamReader(Request.Body).ReadToEndAsync());

            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);

        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _svc.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
