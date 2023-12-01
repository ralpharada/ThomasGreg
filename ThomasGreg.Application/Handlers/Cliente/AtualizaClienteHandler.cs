using ThomasGreg.Application.Core;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Domain.Models;
using ThomasGreg.Application.Services;
using Microsoft.Extensions.Configuration;

namespace ThomasGreg.Application.Handlers
{
    public class AtualizarClienteHandler : IRequestHandler<AtualizarClienteQuery, IEvent>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        public AtualizarClienteHandler(IClienteRepository clienteRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<IEvent> Handle(AtualizarClienteQuery request, CancellationToken cancellationToken)
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

                var emailJaCadastrado = await _clienteRepository.ExisteEmailCadastrado(request.Email, request.Id, cancellationToken);
                if (emailJaCadastrado)
                    return new ResultEvent(success, "Já existe um cadastro associado a este Email.");

                if (String.IsNullOrWhiteSpace(request.Nome))
                    return new ResultEvent(success, "O campo Nome é obrigatório.");

                if (String.IsNullOrWhiteSpace(request.Email))
                    return new ResultEvent(success, "O campo Email é obrigatório.");

                #endregion

                var cliente = await _clienteRepository.ObterPorId(request.Id, cancellationToken);
                cliente.Nome = request.Nome;
                cliente.Email = request.Email;

                if (request.Logotipo != null && !String.IsNullOrWhiteSpace(request.Logotipo.NomeArquivo))
                {
                    var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(request.Logotipo.NomeArquivo);

                    var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, nomeArquivo);

                    if (!Directory.Exists(_configuration.GetSection("FilePath:Cliente.Logotipo").Value))
                        Directory.CreateDirectory(_configuration.GetSection("FilePath:Cliente.Logotipo").Value);
                  
                    SaveBase64Service.SaveBase64ToFile(request.Logotipo.Base64, caminhoLogotipo);
                  
                    var caminhoLogotipoAntigo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, cliente.Logotipo);
                  
                    DeleteFileService.DeleteFile(caminhoLogotipoAntigo);
                  
                    cliente.Logotipo = nomeArquivo;
                }

                var clienteMap = ClienteMapper<Cliente>.Map(cliente);
                var result = await _clienteRepository.Salvar(clienteMap, cancellationToken);
                success = result > 0;

                return new ResultEvent(success, success ? "Cliente atualizado com sucesso!" : "Nenhuma atualização realizada.");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }
        }

    }
}
