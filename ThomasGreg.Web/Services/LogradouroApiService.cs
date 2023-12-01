using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Web.Enums;
using ThomasGreg.Web.Interfaces;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Services
{
    public class LogradouroApiService : ILogradouroApiService
    {
        private readonly IRequisicaoService _requisicaoService;

        public LogradouroApiService(IRequisicaoService requisicaoService)
        {
            _requisicaoService = requisicaoService;
        }

        public async Task<ApiResponse<int>?> AdicionarLogradouro(AdicionarLogradouroQuery query)
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AdicionarLogradouroQuery, int>("usuario", EHttpMethods.POST, query);
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

        public async Task<ApiResponse<int>?> AtualizarLogradouro(AtualizarLogradouroQuery query)
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AtualizarLogradouroQuery, int>("usuario", EHttpMethods.PUT, query);
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

        public async Task<ApiResponse<IEnumerable<LogradouroResponse>>?> ObterLogradouros()
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<object, IEnumerable<LogradouroResponse>>("cliente/listar", EHttpMethods.GET);
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

        public async Task<ApiResponse<LogradouroResponse>?> ObterLogradouroPorId(int id)
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<int, LogradouroResponse>($"cliente/{id}", EHttpMethods.GET);
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
