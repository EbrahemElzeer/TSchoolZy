using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface ITeamMemberService
    {
        Task<IEnumerable<TeamMemberDto>> GetAllAsync();
        Task<TeamMemberDto> GetByIdAsync(int id);
        Task<bool> AddAsync(TeamMemberDto dto);
        Task<bool> UpdateAsync(int id, TeamMemberDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
