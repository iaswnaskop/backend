using System.Security.Claims;

namespace backend.Services.AuthServices
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserRegisterModel request);
        Task<TokenResponseModel?> LoginAsync(UserLoginModel request);
        Task<TokenResponseModel?> RefreshTokensAsync(RefreshTokenRequestModel request);
        Task<UserDetailsModel?> GetUserAsync(ClaimsPrincipal user);
        Task<UserDetailsModel?> UpdateUserDetailsAsync(UserDetailsModel request);

    }
}
