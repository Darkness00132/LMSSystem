using Application.Common.Constants;
using Application.Interfaces.ThirdPartyServices;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.ThirdPartyServices
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateAccessToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new(AppClaims.UserId, user.Id.ToString()),
                new(AppClaims.Email, user.Email!),
                new(AppClaims.FullName, user.FullName),
            };

            foreach (var role in roles)
                claims.Add(new(AppClaims.Role, role));

            if (roles.Contains(UserRole.Assistant.ToString()) && user.InstructorId.HasValue)
                claims.Add(new(AppClaims.InstructorId, user.InstructorId.Value.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}