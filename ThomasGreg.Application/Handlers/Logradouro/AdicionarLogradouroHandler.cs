using ThomasGreg.Application.Core;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Domain.Models;
using MediatR;

namespace ThomasGreg.Application.Handlers
{
    public class AdicionarLogradouroHandler : IRequestHandler<AdicionarLogradouroQuery, IEvent>
    {
        private readonly ILogradouroRepository _logradouroRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public AdicionarLogradouroHandler(ILogradouroRepository logradouroRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _logradouroRepository = logradouroRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(AdicionarLogradouroQuery request, CancellationToken cancellationToken)
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

                if (request.ClienteId <= 0)
                    return new ResultEvent(success, "O campo Cliente é obrigatório.");
                if (String.IsNullOrWhiteSpace(request.NomeRua))
                    return new ResultEvent(success, "O campo Nome da Rua é obrigatório.");
                if (String.IsNullOrWhiteSpace(request.Numero))
                    return new ResultEvent(success, "O campo Numero é obrigatório.");
                if (String.IsNullOrWhiteSpace(request.Bairro))
                    return new ResultEvent(success, "O campo Bairro é obrigatório.");
                if (String.IsNullOrWhiteSpace(request.Cidade))
                    return new ResultEvent(success, "O campo Cidade é obrigatório.");
                if (String.IsNullOrWhiteSpace(request.Estado))
                    return new ResultEvent(success, "O campo Estado é obrigatório.");
                if (String.IsNullOrWhiteSpace(request.Cep))
                    return new ResultEvent(success, "O campo Cep é obrigatório.");

                #endregion

                var logradouro = LogradouroMapper<Logradouro>.Map(request);
                var upsertedId = await _logradouroRepository.Salvar(logradouro, cancellationToken);
                success = upsertedId > 0;

                return new ResultEvent(success, success ? "Logradouro cadastrado com sucesso!" : "Falha ao cadastrar o Logradouro!", null, upsertedId);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
