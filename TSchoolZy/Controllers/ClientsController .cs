using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSchoolZy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClientDto>> GetById(int id)
        {
           
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
                return NotFound();
            return Ok(client);
        }

        // POST: api/Clients
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ClientDto>> Create([FromBody] ClientDto dto)
        {
            dto.IsUpdate = false;
            var result = await _clientService.AddAsync(dto);
            if (!result)
                return BadRequest("Failed to create client.");

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }
        [Authorize]
        // PUT: api/Clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientDto dto)
        {
            dto.IsUpdate = true;
            if (dto.Id != 0 && dto.Id != id)
                return BadRequest("ID mismatch.");

            var updated = await _clientService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }
        [Authorize]
        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _clientService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
