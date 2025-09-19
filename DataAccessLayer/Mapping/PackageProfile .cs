using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using AutoMapper;
using Core.Entity;

namespace Application.Mapping
{
    public class PackageProfile : Profile
    {
        public PackageProfile()
        {
            CreateMap<Package, PackageDto>()
             .ReverseMap(); // مش لازم Ignore هنا

            CreateMap<PackageFeature, PackageFeatureDto>().ReverseMap();
        }
    }

}
