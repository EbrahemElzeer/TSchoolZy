using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface ISettingService
    {
        Task<SettingDto> GetAsync();
        Task<bool> UpdateAsync(SettingDto dto);
        Task<bool> CreateAsync(SettingDto dto);
        Task<bool> DeleteAsync();

    }
}
