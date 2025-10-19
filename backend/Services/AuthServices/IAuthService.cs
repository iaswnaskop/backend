using System.Security.Claims;

namespace backend.Services.AuthServices
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(WooOrderEvent request);
        Task<TokenResponseModel?> LoginAsync(UserLoginModel request);
        Task<TokenResponseModel?> RefreshTokensAsync(RefreshTokenRequestModel request);
        Task<UserDetailsModel?> GetUserAsync(ClaimsPrincipal user);
        Task<UserDetailsModel?> UpdateUserDetailsAsync(UserDetailsModel request);
        Task<(User user, string link)?> RegisterAdminAsync(WooOrderEvent request);

        Task<bool> SetPasswordAsync(WooOrderEvent request);
    }
}
