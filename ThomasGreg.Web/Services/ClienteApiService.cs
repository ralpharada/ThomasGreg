using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Web.Enums;
using ThomasGreg.Web.Interfaces;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Services
{
    public class ClienteApiService : IClienteApiService
    {
        private readonly IRequisicaoService _requisicaoService;

        public ClienteApiService(IRequisicaoService requisicaoService)
        {
            _requisicaoService = requisicaoService;
        }

        public async Task<ApiResponse<string>?> AdicionarCliente(AdicionarClienteQuery query)
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AdicionarClienteQuery, string>("cliente", EHttpMethods.POST, query);
                return apiResponse;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ApiResponse<string>?> AtualizarCliente(AtualizarClienteQuery query)
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AtualizarClienteQuery, string>("cliente", EHttpMethods.PUT, query);
                return apiResponse;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ApiResponse<IEnumerable<ClienteResponse>>?> ObterClientes()
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<object, IEnumerable<ClienteResponse>>("cliente/listar", EHttpMethods.GET);
                return apiResponse;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ApiResponse<ClienteResponse>?> ObterClientePorId(int id)
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<int, ClienteResponse>($"cliente/{id}", EHttpMethods.GET);
                return apiResponse;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
