using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<(bool Succeeded, IEnumerable<string> Errors)> RegisterAsync(RegisterDto model);

        Task<bool> LoginAsync(LoginDto model);
        Task<string> GenerateResetPasswordTokenAsync(ForgotPasswordDto model);
        Task<bool> ResetPasswordAsync(ResetPasswordDto model);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();      
        Task<bool> DeleteUserAsync(string userId);
    }
}
