using System.Security.Claims;

namespace backend.Services.AuthServices
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserModel request);
        Task<TokenResponseModel?> LoginAsync(UserModel request);
        Task<TokenResponseModel?> RefreshTokensAsync(RefreshTokenRequestModel request);
        Task<User?> GetUserAsync(ClaimsPrincipal user);
    }
}
