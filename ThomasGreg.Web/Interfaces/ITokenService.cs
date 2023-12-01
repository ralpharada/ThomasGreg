using ThomasGreg.Domain.Models;

namespace ThomasGreg.Web.Interfaces
{
    public interface ITokenService
    {
        Task<string>? ObterToken();
        string? ObterRefreshToken();
        void SalvarJwt(JwtToken jwtToken);
        Task<string>? RenovarRefreshToken();
        void SalvarRefreshToken(string refreshToken);
        void ExcluirCookies();
    }
}
