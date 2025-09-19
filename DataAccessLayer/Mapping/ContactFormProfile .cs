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
    public class ContactFormProfile : Profile
    {
        public ContactFormProfile()
        {
            CreateMap<ContactFormDto, ContactForm>()
           .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()); // هنعيّنها يدويًا

            CreateMap<ContactForm, ContactFormDto>(); // بيرجع CreatedAt في GET

            // CreateMap<ContactFormDto, ContactForm>()
            //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            //CreateMap<ContactForm, ContactFormDto>().ReverseMap()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            //CreateMap<ContactFormDto, ContactForm>()
            //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }

}
