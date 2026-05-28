using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken ct = default)
        {
            await _context.RefreshTokens.AddAsync(refreshToken, ct);
        }

        public async Task DeleteAsync(RefreshToken refreshToken, CancellationToken ct = default)
        {
             _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync(ct);
        }

        public Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct = default)
        {
            return _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync((rf) => rf.Token == token, ct);
        }
    }
}
