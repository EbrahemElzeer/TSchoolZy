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
    public class ContactFormService : IContactFormService
    {
        private readonly IGenericRepository<ContactForm> _repo;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IGenericRepository<Setting> _settingsRepo;
        public ContactFormService(IGenericRepository<ContactForm> repo, IMapper mapper, IEmailSender emailSender, IGenericRepository<Setting> settingsRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _emailSender = emailSender;
            _settingsRepo = settingsRepo;
        }

        public async Task<IEnumerable<ContactFormDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactFormDto>>(list);
        }

        public async Task<ContactFormDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ContactFormDto>(entity);
        }

        public async Task<bool> AddAsync(ContactFormDto dto)
        {
            var entity = _mapper.Map<ContactForm>(dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(entity);
            var saved = await _repo.SaveChangesAsync();

            if (!saved) return false;

            // 1. Get receiver email from settings
            var setting = (await _settingsRepo.GetAllAsync()).FirstOrDefault();
            var companyEmail = setting?.Email;

            // 2. Send email
            if (!string.IsNullOrEmpty(companyEmail))
            {
                var subject = $"New Contact Message: {dto.Subject}";
                var body = $@"
            <b>From:</b> {dto.Name} ({dto.Email})<br/>
            <b>Message:</b><br/>{dto.Message}
        ";

                await _emailSender.SendEmailAsync(companyEmail, subject, body);
            }

            return true;
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
