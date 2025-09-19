using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interface;
using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositey;


namespace Application.Service
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager,
                           IEmailSender emailSender, IMapper mapper, IConfiguration config) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _mapper = mapper;

            _config = config;
        }

       
        public async Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return (true, Array.Empty<string>());

            var errors = result.Errors.Select(e => e.Description);
            return (false, errors);
        }



        public async Task<bool> LoginAsync(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, false);

            return result.Succeeded;
        }

        public async Task<string> GenerateResetPasswordTokenAsync(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return null;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var frontendUrl = _config["FrontendBaseUrl"];
            var resetLink = $"https://localhost:7153/Dashboard/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";

            await _emailSender.SendEmailAsync(user.Email, "Reset Your Password", $"Click <a href='{resetLink}'>here</a> to reset your password.");

            return token; // يمكن ترجيعه فقط في 
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }


    }

}
