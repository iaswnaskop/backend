using backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace backend.Services.AuthServices
{
    public class AuthService(DataContext context, IConfiguration configuration) : IAuthService
    {
        public async Task<User> RegisterAsync(WooOrderEvent request)
        {
            if (await context.Users.AnyAsync(u => u.Email == request.Customer.Email))
            {
                return null;
            }

            var user = new User();
            //var hashedPassword = new PasswordHasher<User>()
            //    .HashPassword(user, request.Customer.);

            user.Username = request.Customer.Username;
            user.PasswordHash = "123456";
            user.Email = request.Customer.Email;
            user.FullName = request.Customer.FirstName;
            user.Role = "Wp";
            

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<TokenResponseModel?> LoginAsync(UserLoginModel request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseModel> CreateTokenResponse(User? user)
        {
            return new TokenResponseModel
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
                HasPassOnBoarding = user.HasPassOnBoarding
            };
        }


        public async Task<TokenResponseModel?> RefreshTokensAsync(RefreshTokenRequestModel request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (user is null)
                return null;

            return await CreateTokenResponse(user);
        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await context.Users.FindAsync(userId);
            if (user is null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public async Task<UserDetailsModel?> GetUserAsync(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return null;
            var userDetails = await context.Users
                .Include(u => u.Stores)
                .Where(u => u.Id.ToString() == userId)
                .Select(u => new UserDetailsModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    FullName = u.FullName,
                    Stores = u.Stores.Select(s => s.Id).ToList()
                })
                .FirstOrDefaultAsync();
            
            return userDetails;
        }
        public async Task<UserDetailsModel?> UpdateUserDetailsAsync(UserDetailsModel request)
        {
            var user = await context.Users.FindAsync(request.Id);
            if (user is null)
                return null;
            user.Username = request.Username;
            user.Email = request.Email;
            user.FullName = request.FullName;
            user.Phone = request.Phone;
            await context.SaveChangesAsync();
            return new UserDetailsModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.Phone
            };
        }

    }
}
