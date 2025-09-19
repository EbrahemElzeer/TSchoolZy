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
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }

}
