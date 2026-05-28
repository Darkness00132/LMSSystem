using Application.Common.Exceptions;
using Application.Dtos.Auth;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Interfaces.ThirdPartyServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<User> userManager,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenService jwtTokenService,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                _logger.LogWarning("Registration failed - email already in use: {Email}", request.Email);
                throw new ConflictException("Email is already in use.");
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Registration failed for {Email}: {Errors}", request.Email, errors);
                throw new BadRequestException(errors);
            }

            await _userManager.AddToRoleAsync(user, "Student");
            _logger.LogInformation("User registered successfully: {Email}", request.Email);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email)
                ?? throw new UnAuthorizedException("Invalid credentials.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Login failed - invalid password for: {Email}", request.Email);
                throw new UnAuthorizedException("Invalid credentials.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService.GenerateAccessToken(user, roles);
            var refreshToken = await CreateAndSaveRefreshTokenAsync(user.Id);

            _logger.LogInformation("User logged in successfully: {Email}", request.Email);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<AccessTokenResponse> RefreshAsync(RefreshTokenRequest request)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken)
                ?? throw new UnAuthorizedException("Invalid refresh token.");

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                await _refreshTokenRepository.DeleteAsync(storedToken);
                _logger.LogWarning("Refresh attempt with expired token for UserId: {UserId}", storedToken.UserId);
                throw new UnAuthorizedException("Refresh token has expired.");
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId.ToString())
                ?? throw new UnAuthorizedException("User no longer exists.");

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _jwtTokenService.GenerateAccessToken(user, roles);

            _logger.LogInformation("Access token refreshed for UserId: {UserId}", user.Id);

            return new AccessTokenResponse { AccessToken = newAccessToken };
        }

        public async Task LogoutAsync(RefreshTokenRequest request)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken)
                ?? throw new NotFoundException("Refresh token not found.");

            await _refreshTokenRepository.DeleteAsync(storedToken);
            _logger.LogInformation("User logged out - token deleted for UserId: {UserId}", storedToken.UserId);
        }

        // ── private helpers ────────────────────────────────────────────

        private async Task<string> CreateAndSaveRefreshTokenAsync(Guid userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            return refreshToken.Token;
        }
    }
}