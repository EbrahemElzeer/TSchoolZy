using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface IClientService
    {

        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<ClientDto> GetByIdAsync(int id);
        Task<bool> AddAsync(ClientDto dto);
        Task<bool> UpdateAsync(int id, ClientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
