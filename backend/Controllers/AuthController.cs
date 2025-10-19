using backend.Services;
using backend.Services.AuthServices;
using backend.Services.EmailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;


namespace backend.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            this.authService = authService;
            _emailService = emailService;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("register-bywp")]
        public async Task<ActionResult<WooOrderEvent>> Register(WooOrderEvent request)
        {
            if (request == null)
                return BadRequest("Json is null");


            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            var response = new Customer
            {
                Email = user.Email,
                Username = user.Username

            };

            return Ok(response);
        }

        [HttpPost("register-admin")]
        public async Task<ActionResult<WooOrderEvent>> RegisterAdmin(WooOrderEvent request)
        {
            if (request == null)
                return BadRequest("Json is null");
            var user = await authService.RegisterAdminAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            var response = user.GetValueOrDefault();
            

            await _emailService.SendEmailAsync(response.user.Email, "Set your password",
                $"<p>Please set your password by clicking <a href='{response.link}'>here</a>.</p>");

            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromQuery] string toEmail)
        {
            await _emailService.SendEmailAsync(toEmail, "ΣΑΣ ΨΑΧΝΟΥΜΕ ΑΠΟ ΤΗΝ LOUNDLINK, ΦΛΩΡΟΙ ΤHΣ LOCKALLY", "<h1>ΑΥΡΙΟ ΘΑ ΠΕΣΕΙ ΞΥΛΟ ΚΑΙ ΠΟΥΤΣΑ</h1>");
            return Ok("Email sent successfully!");
        }

        [HttpPost("set-password")]
        public async Task<ActionResult> SetPassword(WooOrderEvent request)
        {
            var result = await authService.SetPasswordAsync(request);
            if (!result)
                return BadRequest("Invalid token or token has expired.");

            return Ok("Password has been set successfully.");
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
