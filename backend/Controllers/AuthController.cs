using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using backend.Services.AuthServices;

namespace backend.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserModel request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseModel>> Login(UserModel request)
        {
            var result = await authService.LoginAsync(request);
            if (result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseModel>> RefreshToken(RefreshTokenRequestModel request)
        {
            var result = await authService.RefreshTokensAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
                return Unauthorized("Invalid refresh token.");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("user-details")]
        public async Task<ActionResult<User>> UserDetails()
        {
            var user = await authService.GetUserAsync(User);
            if (user is null)
                return NotFound("User not found.");
            return Ok(user);
        }
    }
}
