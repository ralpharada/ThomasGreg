using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Interfaces
{
    public interface ILogradouroApiService
    {
        Task<ApiResponse<IEnumerable<LogradouroResponse>>?> ObterLogradouros();
        Task<ApiResponse<LogradouroResponse>?> ObterLogradouroPorId(int id);
        Task<ApiResponse<int>?> AdicionarLogradouro(AdicionarLogradouroQuery cliente);
        Task<ApiResponse<int>?> AtualizarLogradouro(AtualizarLogradouroQuery cliente);
    }
}
