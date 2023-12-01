using ThomasGreg.Application.Core;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Core.Events;
using MediatR;
using ThomasGreg.Domain.Interfaces;

namespace ThomasGreg.Application.Handlers
{
    public class SelecionarLogradouroHandler : IRequestHandler<SelecionarLogradouroQuery, IEvent>
    {
        private readonly ILogradouroRepository _logradouroRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public SelecionarLogradouroHandler(ILogradouroRepository logradouroRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _logradouroRepository = logradouroRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(SelecionarLogradouroQuery request, CancellationToken cancellationToken)
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

                var logradouro = await _logradouroRepository.ObterPorId(request.Id,request.ClienteId, cancellationToken);
                var logradouroMap = LogradouroMapper<LogradouroResponse>.Map(logradouro);
                if (logradouro == null)
                    return new ResultEvent(success, "Logradouro não localizado.");

                return new ResultEvent(true, logradouroMap);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
