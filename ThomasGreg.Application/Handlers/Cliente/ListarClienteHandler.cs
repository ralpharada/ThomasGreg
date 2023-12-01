using ThomasGreg.Application.Core;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;
using ThomasGreg.Application.Services;
using Microsoft.Extensions.Configuration;

namespace ThomasGreg.Application.Handlers
{
    public class ListarClienteHandler : IRequestHandler<ListarClienteQuery, IEvent>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public ListarClienteHandler(IClienteRepository clienteRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<IEvent> Handle(ListarClienteQuery request, CancellationToken cancellationToken)
        {
            var response = new List<ClienteResponse>();
            try
            {
                #region Validação do usuário autenticado

                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(false, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(false, "Acesso expirado");

                #endregion

                var lista = await _clienteRepository.Listar(cancellationToken);
                foreach(var cliente in lista)
                {
                    var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, cliente.Logotipo);
                    cliente.Logotipo = FileToBase64Service.FileToBase64(caminhoLogotipo);
                }
                response = ClienteMapper<List<ClienteResponse>>.Map(lista);

                return new ResultEvent(true, response, lista.Count());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}
