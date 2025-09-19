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
    public class SettingService : ISettingService
    {
        private readonly IGenericRepository<Setting> _repo;
        private readonly IMapper _mapper;

        public SettingService(IGenericRepository<Setting> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SettingDto> GetAsync()
        {
            var entity = (await _repo.GetAllAsync()).FirstOrDefault();
            if (entity == null) return null;
            return _mapper.Map<SettingDto>(entity);
        }

        public async Task<bool> UpdateAsync(SettingDto dto)
        {
            var entity = (await _repo.GetAllAsync()).FirstOrDefault();
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _repo.Update(entity);
            return await _repo.SaveChangesAsync();
        }
        public async Task<bool> CreateAsync(SettingDto dto)
        {
            var entity = _mapper.Map<Setting>(dto);
            await _repo.AddAsync(entity);
            return await _repo.SaveChangesAsync();
        }

    

    public async Task<bool> DeleteAsync()
        {
            var entity = (await _repo.GetAllAsync()).FirstOrDefault();
            if (entity == null) return false;

            _repo.Delete(entity);
            return await _repo.SaveChangesAsync();
        }
    }

    }
