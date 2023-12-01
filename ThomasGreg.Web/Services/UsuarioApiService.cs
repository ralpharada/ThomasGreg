using ThomasGreg.Application.Queries;
using ThomasGreg.Web.Enums;
using ThomasGreg.Web.Interfaces;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Services
{
    public class UsuarioApiService : IUsuarioApiService
    {
        private readonly IRequisicaoService _requisicaoService;
        public UsuarioApiService(IRequisicaoService requisicaoService)
        {
            _requisicaoService = requisicaoService;
        }

        public async Task<ApiResponse<string>> AdicionarUsuario(AdicionarUsuarioQuery query)
        {
            var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AdicionarUsuarioQuery, string>("usuario", EHttpMethods.POST, query);
            return apiResponse;
        }

        public async Task<ApiResponse<string>> AtualizarUsuario(AtualizarUsuarioQuery query)
        {
            var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AtualizarUsuarioQuery, string>("usuario", EHttpMethods.PUT, query);
            return apiResponse;
        }

       
    }

}
