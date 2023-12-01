using ThomasGreg.Application.Core;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;
using ThomasGreg.Application.Services;
using Microsoft.Extensions.Configuration;

namespace ThomasGreg.Application.Handlers
{
    public class ExcluirClienteHandler : IRequestHandler<ExcluirClienteQuery, IEvent>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public ExcluirClienteHandler(IClienteRepository clienteRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<IEvent> Handle(ExcluirClienteQuery request, CancellationToken cancellationToken)
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

                #region Validação dos campos

                var cliente = await _clienteRepository.ObterPorId(request.Id, cancellationToken);
                if (cliente == null)
                    return new ResultEvent(success, "Cliente não localizado.");

                #endregion

                success = await _clienteRepository.ExcluirPorId(request.Id, cancellationToken);
                if (success)
                {
                    var caminhoLogotipoAntigo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, cliente.Logotipo);

                    DeleteFileService.DeleteFile(caminhoLogotipoAntigo);
                }
                return new ResultEvent(success, success ? "Cliente excluído com sucesso!" : "Falha ao excluir o cliente!");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
