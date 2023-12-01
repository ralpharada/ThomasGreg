using ThomasGreg.Application.Core;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;

namespace ThomasGreg.Application.Handlers
{
    public class ExcluirLogradouroHandler : IRequestHandler<ExcluirLogradouroQuery, IEvent>
    {
        private readonly ILogradouroRepository _logradouroRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public ExcluirLogradouroHandler(ILogradouroRepository logradouroRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _logradouroRepository = logradouroRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(ExcluirLogradouroQuery request, CancellationToken cancellationToken)
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

                var verificacao = await _logradouroRepository.ObterPorId(request.Id, request.ClienteId, cancellationToken);
                if (verificacao == null)
                    return new ResultEvent(success, "Logradouro não localizado.");

                #endregion

                success = await _logradouroRepository.ExcluirPorId(request.Id, request.ClienteId, cancellationToken);

                return new ResultEvent(success, success ? "Logradouro excluído com sucesso!" : "Falha ao excluir o logradouro!");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
