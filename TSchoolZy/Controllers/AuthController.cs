using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TSchoolZy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize]
        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (succeeded, errors) = await _userService.RegisterAsync(model);

            if (!succeeded)
                return BadRequest(new { Errors = errors });

            return Ok("User registered successfully");
        }


        // POST: api/auth/login
        [HttpPost("login")]
       
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.LoginAsync(model);

            if (!result)
                return Unauthorized("Invalid login credentials");

            return Ok("Login successful");
        }

        //[Authorize]
        // POST: api/auth/forgot-password
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _userService.GenerateResetPasswordTokenAsync(model);

            if (string.IsNullOrEmpty(token))
                return NotFound("User with this email does not exist");

            return Ok("Reset password email sent");
        }

        // POST: api/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ResetPasswordAsync(model);

            if (!result)
                return BadRequest("Password reset failed");

            return Ok("Password has been reset successfully");
        }
        //[AllowAnonymous]

        //[HttpGet("antiforgery/token")]
        //public IActionResult GetAntiforgeryToken([FromServices] IAntiforgery antiforgery)
        //{
        //    var tokens = antiforgery.GetAndStoreTokens(HttpContext);
        //    return Ok(new AntiforgeryTokenDto { Token = tokens.RequestToken });
        //}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var success = await _userService.DeleteUserAsync(id);

            if (!success)
                return NotFound($"User with id {id} not found.");

            return Ok($"User with id {id} deleted successfully.");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}

