using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThomasGreg.Application.Queries;
using ThomasGreg.Web.Interfaces;
using ThomasGreg.Web.Models;
using ThomasGreg.Web.Attributes;

namespace ThomasGreg.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IAutenticacaoUsuarioApiService _autenticacaoUsuarioApiService;
        private readonly ITokenService _tokenService;
        private readonly IUsuarioApiService _usuarioApiService;
        public HomeController(ILogger<ClienteController> logger,
            IAutenticacaoUsuarioApiService autenticacaoUsuarioApiService,
            ITokenService tokenService,
            IUsuarioApiService usuarioApiService) : base()
        {
            _logger = logger;
            _autenticacaoUsuarioApiService = autenticacaoUsuarioApiService;
            _tokenService = tokenService;
            _usuarioApiService = usuarioApiService;
        }
        [ServiceFilter(typeof(VerificarAutenticacaoAttribute))]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var query = new AutenticacaoUsuarioQuery(username, password);
            var loginOk = await _autenticacaoUsuarioApiService.AutenticacaoUsuario(query);
            if (loginOk)
                return RedirectToAction("Index", "Home");
            ViewBag.ErrorMessage = "E-mail ou senha inválidos.";
            return View();
        }
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            _tokenService.ExcluirCookies();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("obterToken")]
        public async Task<string> ObterToken()
        {
            return await _tokenService.ObterToken();
        }
        [HttpGet]
        [Route("Cadastro")]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [Route("Cadastro")]
        public async Task<IActionResult> Cadastro(UsuarioCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                var novoCadastro = new AdicionarUsuarioQuery(model.Nome, model.Email, true, model.Senha);
                var result = await _usuarioApiService.AdicionarUsuario(novoCadastro);
                if (result != null && result.Success)
                {
                    var query = new AutenticacaoUsuarioQuery(model.Email, model.Senha);
                    var loginOk = await _autenticacaoUsuarioApiService.AutenticacaoUsuario(query);
                    if (loginOk)
                        return RedirectToAction("Index", "Home");
                }
                ViewBag.Erro = result;
            }
            return View(model);
        }
    }
}
