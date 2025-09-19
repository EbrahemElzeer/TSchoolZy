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
    public class ClientService:IClientService
    {
        private readonly IGenericRepository<Client> _repository;
        private readonly IMapper _mapper;
        public ClientService(IGenericRepository<Client> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
     

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var Client = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(Client);
        }

        public async Task<ClientDto> GetByIdAsync(int id)
        {
            var Client = await _repository.GetByIdAsync(id);
            return _mapper.Map<ClientDto>(Client);
        }

        public async Task<bool> AddAsync(ClientDto dto)
        {
            var entity = _mapper.Map<Client>(dto);
            await _repository.AddAsync(entity);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, ClientDto dto)
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
