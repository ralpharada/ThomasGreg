using ThomasGreg.Domain.Models;

namespace ThomasGreg.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> ObterPorChaveUsuario(string refreshToken);
        Task AtualizarPorUsuarioId(RefreshToken refreshToken);
    }
}
