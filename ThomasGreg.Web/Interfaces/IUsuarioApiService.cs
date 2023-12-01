using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Interfaces
{
    public interface IUsuarioApiService
    {
        Task<ApiResponse<string>> AdicionarUsuario(AdicionarUsuarioQuery cliente);
        Task<ApiResponse<string>> AtualizarUsuario(AtualizarUsuarioQuery cliente);
    }
}
