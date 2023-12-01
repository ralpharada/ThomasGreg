using ThomasGreg.Application.Core;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Core.Events;
using MediatR;
using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Application.Services;
using Microsoft.Extensions.Configuration;

namespace ThomasGreg.Application.Handlers
{
    public class SelecionarClienteHandler : IRequestHandler<SelecionarClienteQuery, IEvent>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public SelecionarClienteHandler(IClienteRepository clienteRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<IEvent> Handle(SelecionarClienteQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                #region Validação do usuário autenticado

                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(success, "Acesso expirado");

                #endregion

                var cliente = await _clienteRepository.ObterPorId(request.Id, cancellationToken);
                var clienteMap = ClienteMapper<ClienteResponse>.Map(cliente);
                if (cliente == null)
                    return new ResultEvent(success, "Cliente não localizado.");
              
                var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, clienteMap.Logotipo);
                clienteMap.Logotipo = FileToBase64Service.FileToBase64(caminhoLogotipo);
              
                return new ResultEvent(true, clienteMap);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
