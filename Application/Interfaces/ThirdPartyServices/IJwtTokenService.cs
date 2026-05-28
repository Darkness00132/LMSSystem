using Domain.Entities;

namespace Application.Interfaces.ThirdPartyServices
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user, IList<string> roles);
    }
}
