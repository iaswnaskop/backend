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
        //[Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterModel>> Register(UserRegisterModel request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            var response = new UserRegisterModel
            {
                
                Username = user.Username,
                Email = user.Email
            };

            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseModel>> Login(UserLoginModel request)
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
        public async Task<ActionResult<UserDetailsModel>> UserDetails()
        {
            var user = await authService.GetUserAsync(User);
            if (user is null)
                return NotFound("User not found.");
            return Ok(user);
        }


        [Authorize]
        [HttpPost("update-user-details")]
        public async Task<ActionResult<UserDetailsModel>> UpdateUserDetails(UserDetailsModel request)
        {
            var user = await authService.UpdateUserDetailsAsync(request);
            if (user is null)
                return NotFound("User not found.");
            return Ok(user);
        }

        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return Ok("Test endpoint is working!");
        }

    }
}
