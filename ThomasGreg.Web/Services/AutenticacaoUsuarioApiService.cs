using Newtonsoft.Json;
using System.Net.Http.Json;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Enums;
using ThomasGreg.Web.Interfaces;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Services
{
    public class AutenticacaoUsuarioApiService : IAutenticacaoUsuarioApiService
    {
        private readonly ITokenService _tokenService;
        private readonly IRequisicaoService _requisicaoService;

        public AutenticacaoUsuarioApiService(
           ITokenService tokenService,
           IRequisicaoService requisicaoService)
        {
            _tokenService = tokenService;
            _requisicaoService = requisicaoService;
        }

        public async Task<bool> AutenticacaoUsuario(AutenticacaoUsuarioQuery autenticacaoUsuario)
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<AutenticacaoUsuarioQuery, object>("usuario/login", EHttpMethods.POST, autenticacaoUsuario);

                if (apiResponse.Success)
                {
                    string jsonData = JsonConvert.SerializeObject(apiResponse.Data);
                    var jwtToken = JsonConvert.DeserializeObject<JwtToken>(jsonData);
                    _tokenService.SalvarJwt(jwtToken);
                }
                return apiResponse.Success;
            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<ApiResponse<UsuarioLogadoResponse>> ObterUsuarioLogado()
        {
            try
            {
                var apiResponse = await _requisicaoService.EnviarRequisicaoAutenticada<object, UsuarioLogadoResponse>("usuario/obterLogado", EHttpMethods.GET);
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
