using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface IPackageService
    {
        //Task<IEnumerable<PackageDto>> GetAllAsync();
        Task<List<PackageDto>> GetAllAsync();
        Task<PackageDto> GetByIdAsync(int id);
        //Task<PackageDto> GetByIdAsync(int id);
        Task<bool> AddAsync(PackageDto dto);
        Task<bool> UpdateAsync(int id, PackageDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
