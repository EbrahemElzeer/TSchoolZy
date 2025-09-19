using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interface;
using AutoMapper;
using Core.Entity;
using Core.Interface;

namespace Application.Service
{
    public class TeamMemberService:ITeamMemberService
    {
        private readonly IGenericRepository<TeamMember> _repository;
        private readonly IMapper _mapper;

        public TeamMemberService(IGenericRepository<TeamMember> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamMemberDto>> GetAllAsync()
        {
            var members = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeamMemberDto>>(members);
        }

        public async Task<TeamMemberDto> GetByIdAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);
            return _mapper.Map<TeamMemberDto>(member);
        }

        public async Task<bool> AddAsync(TeamMemberDto dto)
        {
            var entity = _mapper.Map<TeamMember>(dto);
            await _repository.AddAsync(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, TeamMemberDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing); // Update existing entity
            _repository.Update(existing);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            _repository.Delete(existing);
            return await _repository.SaveChangesAsync();
        }
    }
}
