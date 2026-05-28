using Application.Dtos.Auth;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest registerDto);

        Task<AuthResponse> LoginAsync(LoginRequest request);

        Task<AccessTokenResponse> RefreshAsync(RefreshTokenRequest request);

        Task LogoutAsync(RefreshTokenRequest request);
    }
}
