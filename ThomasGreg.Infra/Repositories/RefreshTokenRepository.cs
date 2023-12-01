using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ThomasGreg.Infra.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AtualizarPorUsuarioId(RefreshToken refreshToken)
        {
            var currentRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.UsuarioId.Equals(refreshToken.UsuarioId));
            if (currentRefreshToken != null)
            {
                _context.RefreshTokens.Remove(currentRefreshToken);
            }
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }
        public async Task<RefreshToken> ObterPorChaveUsuario(string refreshToken)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);
        }
    }
}
