using ThomasGreg.Web.Enums;
using ThomasGreg.Web.Responses;

namespace ThomasGreg.Web.Interfaces
{
    public interface IRequisicaoService
    {
        Task<HttpResponseMessage> EnviarRequisicaoSemAutenticacao<TRequest, TResponse>(string endpoint, EHttpMethods httpMethods, TRequest serialize = default);
        Task<ApiResponse<TResponse>?> EnviarRequisicaoAutenticada<TRequest, TResponse>(string endpoint, EHttpMethods httpMethods, TRequest serialize = default);

    }
}
