using Application.Dtos.Auth;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LMSSystem.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Creates a new account for the user.
        /// </summary>
        /// <param name="request">User registration information.</param>
        /// <returns>Returns success if the account was created.</returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            await _authService.RegisterAsync(request);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Logs in a mobile/native client and returns access and refresh tokens.
        /// </summary>
        /// <param name="request">User login credentials.</param>
        /// <returns>JWT access token and refresh token.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(
            LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            return Ok(response);
        }

        /// <summary>
        /// Logs in a web client and stores the refresh token in a secure cookie.
        /// </summary>
        /// <param name="request">User login credentials.</param>
        /// <returns>JWT access token.</returns>
        [HttpPost("login-web")]
        public async Task<ActionResult<AccessTokenResponse>> LoginWeb(
            LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            SetRefreshTokenCookie(response.RefreshToken);

            return Ok(new AccessTokenResponse
            {
                AccessToken = response.AccessToken
            });
        }

        /// <summary>
        /// Generates a new access token using a valid refresh token.
        /// </summary>
        /// <param name="request">
        /// Refresh token from request body. 
        /// If not provided, the refresh token cookie will be used.
        /// </param>
        /// <returns>New JWT access token.</returns>
        [HttpPost("refresh")]
        public async Task<ActionResult<AccessTokenResponse>> Refresh(
            RefreshTokenRequest? request)
        {
            var refreshToken =
                request?.RefreshToken ??
                Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            var response = await _authService.RefreshAsync(
                new RefreshTokenRequest
                {
                    RefreshToken = refreshToken
                });

            return Ok(response);
        }

        /// <summary>
        /// Logs out the user by removing the refresh token.
        /// </summary>
        /// <param name="request">
        /// Refresh token from request body. 
        /// If not provided, the refresh token cookie will be used.
        /// </param>
        /// <returns>No content if logout succeeds.</returns>
        [HttpDelete("logout")]
        public async Task<ActionResult> Logout(
            RefreshTokenRequest? request)
        {
            var refreshToken =
                request?.RefreshToken ??
                Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized();

            await _authService.LogoutAsync(
                new RefreshTokenRequest
                {
                    RefreshToken = refreshToken
                });

            Response.Cookies.Delete("refreshToken");

            return NoContent();
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append(
                "refreshToken",
                refreshToken,
                cookieOptions
            );
        }
    }
}