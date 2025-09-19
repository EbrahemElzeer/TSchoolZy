using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSchoolZy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberService _teamMemberService;

        public TeamMemberController(ITeamMemberService teamMemberService)
        {
            _teamMemberService = teamMemberService;
        }

        // GET: api/TeamMember

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamMemberDto>>> GetAll()
        {
            var teamMembers = await _teamMemberService.GetAllAsync();
            return Ok(teamMembers);
        }

        // GET: api/TeamMember/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamMemberDto>> GetById(int id)
        {
            var member = await _teamMemberService.GetByIdAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        // POST: api/TeamMember
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TeamMemberDto dto)
        {
            dto.IsUpdate = false;
            var result = await _teamMemberService.AddAsync(dto);
            if (!result)
                return BadRequest("Failed to create team member.");

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // PUT: api/TeamMember/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TeamMemberDto dto)
        {
            dto.IsUpdate = true;
            if (id != dto.Id && dto.Id != 0) 
                return BadRequest("ID mismatch");
            var result = await _teamMemberService.UpdateAsync(id, dto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/TeamMember/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _teamMemberService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

