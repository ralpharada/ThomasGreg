using ThomasGreg.Application.Core;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;

namespace ThomasGreg.Application.Handlers
{
    public class SelecionarUsuarioHandler : IRequestHandler<SelecionarUsuarioQuery, IEvent>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public SelecionarUsuarioHandler(IUsuarioRepository usuarioRepository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(SelecionarUsuarioQuery request, CancellationToken cancellationToken)
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

                var usuario = await _usuarioRepository.ObterPorId(request.Id, cancellationToken);
         
                if (usuario == null)
                    return new ResultEvent(success, "Usuario não localizado.");

                return new ResultEvent(true, usuario);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
