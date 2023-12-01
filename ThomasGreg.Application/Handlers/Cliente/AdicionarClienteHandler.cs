using ThomasGreg.Application.Core;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using ThomasGreg.Application.Services;

namespace ThomasGreg.Application.Handlers
{
    public class AdicionarClienteHandler : IRequestHandler<AdicionarClienteQuery, IEvent>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AdicionarClienteHandler(IClienteRepository clienteRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository,
            IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<IEvent> Handle(AdicionarClienteQuery request, CancellationToken cancellationToken)
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

                var emailJaCadastrado = await _clienteRepository.ExisteEmailCadastrado(request.Email, 0, cancellationToken);
                if (emailJaCadastrado)
                    return new ResultEvent(success, "Já existe um cadastro associado a este Email.");

                if (String.IsNullOrWhiteSpace(request.Nome))
                    return new ResultEvent(success, "O campo Nome é obrigatório.");

                if (String.IsNullOrWhiteSpace(request.Email))
                    return new ResultEvent(success, "O campo Email é obrigatório.");

                if (String.IsNullOrWhiteSpace(request.Logotipo.Base64))
                    return new ResultEvent(success, "O campo Logotipo é obrigatório.");

                #endregion

                var cliente = ClienteMapper<Cliente>.Map(request);

                var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(request.Logotipo.NomeArquivo);

                var caminhoLogotipo = Path.Combine(_configuration.GetSection("FilePath:Cliente.Logotipo").Value, nomeArquivo);

                if (!Directory.Exists(_configuration.GetSection("FilePath:Cliente.Logotipo").Value))
                    Directory.CreateDirectory(_configuration.GetSection("FilePath:Cliente.Logotipo").Value);

                SaveBase64Service.SaveBase64ToFile(request.Logotipo.Base64, caminhoLogotipo);

                cliente.Logotipo = nomeArquivo;

                var upsertedId = await _clienteRepository.Salvar(cliente, cancellationToken);
                success = upsertedId > 0;

                return new ResultEvent(success, success ? "Cliente cadastrado com sucesso!" : "Falha ao cadastrar o Cliente!", null, upsertedId);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
