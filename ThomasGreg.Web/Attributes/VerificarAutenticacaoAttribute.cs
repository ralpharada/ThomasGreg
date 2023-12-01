using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Web.Interfaces;

namespace ThomasGreg.Web.Attributes
{
    public class VerificarAutenticacaoAttribute : ActionFilterAttribute
    {
        private readonly IAutenticacaoUsuarioApiService _autenticacaoUsuarioApiService;

        public VerificarAutenticacaoAttribute(IAutenticacaoUsuarioApiService autenticacaoUsuarioApiService)
        {
            _autenticacaoUsuarioApiService = autenticacaoUsuarioApiService;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var usuarioLogado = await _autenticacaoUsuarioApiService.ObterUsuarioLogado();

            if (usuarioLogado == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                await next();
            }
        }
    }
}
