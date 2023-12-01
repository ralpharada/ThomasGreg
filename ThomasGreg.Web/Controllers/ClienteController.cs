using Microsoft.AspNetCore.Mvc;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Domain.Models;
using ThomasGreg.Web.Attributes;
using ThomasGreg.Web.Interfaces;
using ThomasGreg.Web.Models;
using ThomasGreg.Web.Services;

namespace ThomasGreg.Web.Controllers
{
    [ServiceFilter(typeof(VerificarAutenticacaoAttribute))]
    public class ClienteController : BaseController
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IClienteApiService _clienteApiService;
        public ClienteController(ILogger<ClienteController> logger,
           IClienteApiService clienteApiService) : base()
        {
            _logger = logger;
            _clienteApiService = clienteApiService;
        }

        public async Task<IActionResult> Index()
        {
            var clientesResponse = await _clienteApiService.ObterClientes();

            if (clientesResponse != null && clientesResponse.Success)
                return View(clientesResponse.Data);

            _logger.LogError($"Erro ao obter clientes da API: {clientesResponse.Data}");
            return View("Error");
        }
        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return base.Erro404();

            var clientesResponse = await _clienteApiService.ObterClientePorId(id.Value);
            if (clientesResponse == null)
                return base.Erro404();

            if (clientesResponse == null || !clientesResponse.Success)
            {
                _logger.LogError($"Erro ao obter clientes da API: {clientesResponse?.Data}");
                return View("Error");
            }

            return View(clientesResponse.Data);
        }

        // GET: Cliente/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,Logotipo")] AdicionarClienteFormModel formModel)
        {
            var query = new AdicionarClienteQuery("", "", null);
            if (ModelState.IsValid)
            {
                var arquivo = await ConvertFileService.ConvertFileToBase64(formModel.Logotipo);
                query = new AdicionarClienteQuery(formModel.Nome, formModel.Email, new Arquivo { NomeArquivo = formModel.Logotipo.FileName, Base64 = arquivo });

                var clientesResponse = await _clienteApiService.AdicionarCliente(query);
                if (clientesResponse.Success)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = clientesResponse;
            }
            return View(query);

        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return base.Erro404();

            var clientesResponse = await _clienteApiService.ObterClientePorId(id.Value);
            if (clientesResponse == null)
                return base.Erro404();

            if (clientesResponse == null || !clientesResponse.Success)
            {
                _logger.LogError($"Erro ao obter clientes da API: {clientesResponse?.Data}");
                return View("Error");
            }

            return View(clientesResponse.Data);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Logotipo")] AtualizarClienteFormModel formModel)
        {
            if (id != formModel.Id)
                return base.Erro404();

            if (ModelState.IsValid)
            {
                var arquivo = formModel.Logotipo != null ? await ConvertFileService.ConvertFileToBase64(formModel.Logotipo) : String.Empty;

                var logotipo = formModel.Logotipo != null ? new Arquivo { NomeArquivo = formModel.Logotipo.FileName, Base64 = arquivo }
                : new Arquivo { NomeArquivo = String.Empty, Base64 = String.Empty };

                var query = new AtualizarClienteQuery(formModel.Id, formModel.Nome, formModel.Email, logotipo);

                var atualizarResponse = await _clienteApiService.AtualizarCliente(query);
                if (atualizarResponse == null)
                    return base.Erro404();

                if (atualizarResponse.Success)
                    return RedirectToAction(nameof(Index));
                ViewBag.Erro = atualizarResponse;

            }
            var clienteResponse = await _clienteApiService.ObterClientePorId(id);
            var response = new ClienteResponse { Nome = formModel.Nome, Email = formModel.Email, Logotipo = clienteResponse.Data.Logotipo };
            return View(response);
        }
    }
}