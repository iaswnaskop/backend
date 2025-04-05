using System.Security.Claims;

namespace backend.Services.AuthServices
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserRegisterModel request);
        Task<TokenResponseModel?> LoginAsync(UserLoginModel request);
        Task<TokenResponseModel?> RefreshTokensAsync(RefreshTokenRequestModel request);
        Task<User?> GetUserAsync(ClaimsPrincipal user);
        Task<User?> GetUserById(Guid id);
    }
}
