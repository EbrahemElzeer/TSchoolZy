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
using Repositey.GenericRepository;

namespace Application.Service
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IGenericRepository<Package> _repo;
        private readonly IMapper _mapper;

        public PackageService(IGenericRepository<Package> repo, IMapper mapper, IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
            _repo = repo;
            _mapper = mapper;
        }

        //public async Task<IEnumerable<PackageDto>> GetAllAsync()
        //{
        //    var list = await _repo.GetAllAsync();
        //    return _mapper.Map<IEnumerable<PackageDto>>(list);
        //}

        //public async Task<PackageDto> GetByIdAsync(int id)
        //{
        //    var entity = await _repo.GetByIdAsync(id);
        //    if (entity == null) return null;
        //    return _mapper.Map<PackageDto>(entity);
        //}
        public async Task<PackageDto> GetByIdAsync(int id)
        {
            var package = await _packageRepository.GetByIdWithFeaturesAsync(id);
            if (package == null) return null;

            return _mapper.Map<PackageDto>(package);
        }

        public async Task<List<PackageDto>> GetAllAsync()
        {
            var packages = await _packageRepository.GetAllWithFeaturesAsync();
            return _mapper.Map<List<PackageDto>>(packages);
        }


        public async Task<bool> AddAsync(PackageDto dto)
        {
            var entity = _mapper.Map<Package>(dto);
            if (entity.Features != null && entity.Features.Any())
            {
                foreach (var feature in entity.Features)
                {
                    feature.Package = entity; // مهم جداً!
                }
            }
            await _repo.AddAsync(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(int id, PackageDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            _repo.Update(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            return await _repo.SaveChangesAsync();
        }
    }

}
