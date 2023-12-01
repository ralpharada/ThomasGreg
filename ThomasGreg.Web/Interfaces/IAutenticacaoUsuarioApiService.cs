using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Core.Events;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Interfaces
{
    public interface IAutenticacaoUsuarioApiService
    {
        Task<bool> AutenticacaoUsuario(AutenticacaoUsuarioQuery autenticacaoUsuario);
        Task<ApiResponse<UsuarioLogadoResponse>> ObterUsuarioLogado();
    }
}
