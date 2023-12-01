using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Core.Events;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Interfaces
{
    public interface IClienteApiService
    {
        Task<ApiResponse<IEnumerable<ClienteResponse>>?> ObterClientes();
        Task<ApiResponse<ClienteResponse>?> ObterClientePorId(int id);
        Task<ApiResponse<string>?> AdicionarCliente(AdicionarClienteQuery cliente);
        Task<ApiResponse<string>?> AtualizarCliente(AtualizarClienteQuery cliente);
    }
}
