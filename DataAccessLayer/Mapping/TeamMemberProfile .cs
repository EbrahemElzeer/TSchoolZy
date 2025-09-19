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
    public class TeamMemberProfile : Profile
    {
        public TeamMemberProfile()
        {
            CreateMap<TeamMemberDto, TeamMember>()
     .ForMember(dest => dest.Id, opt => opt.Ignore())
.ReverseMap();
        }
    }
}
