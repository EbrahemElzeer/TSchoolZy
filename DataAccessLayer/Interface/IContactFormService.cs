using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface IContactFormService
    {
        Task<IEnumerable<ContactFormDto>> GetAllAsync();
        Task<ContactFormDto> GetByIdAsync(int id);
        Task<bool> AddAsync(ContactFormDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
