using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default);
        Task AddAsync(RefreshToken refreshToken, CancellationToken ct = default);
        Task DeleteAsync(RefreshToken refreshToken, CancellationToken ct = default);
    }
}
