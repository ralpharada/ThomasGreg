using ThomasGreg.Application.Core;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Responses;

namespace ThomasGreg.Application.Handlers
{
    public class ObterUsuarioLogadoHandler : IRequestHandler<ObterUsuarioLogadoQuery, IEvent>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public ObterUsuarioLogadoHandler(IUsuarioRepository usuarioRepository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(ObterUsuarioLogadoQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuario = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuario == null)
                    return new ResultEvent(success, "Acesso expirado");

                var usuarioMap = UsuarioMapper<UsuarioLogadoResponse>.Map(usuario);
                return new ResultEvent(true, usuarioMap);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
