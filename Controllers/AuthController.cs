using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Owhytee_Phones.Core.Application.Interface.Service;
using Owhytee_Phones.Models.AuthModel;
using System.Security.Claims;

namespace Owhytee_Phones.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(response);
        }

        [HttpPost("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userClaims = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaims == null || !int.TryParse(userClaims.Value, out int userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }
            var result = await _authService.ChangePasswordAsync(userId,request);
            if (!result)
            {
                return BadRequest(new { message = "Password change failed" });
            }
            return Ok(new { message = "Password changed successfully" });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userClaims = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userClaims == null || !int.TryParse(userClaims.Value, out int userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }
            var user = await _authService.GetUserIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(user);
        }
    }
}
