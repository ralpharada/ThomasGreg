using ThomasGreg.Application.Core;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;

namespace ThomasGreg.Application.Handlers
{
    public class ListarLogradouroHandler : IRequestHandler<ListarLogradouroQuery, IEvent>
    {
        private readonly ILogradouroRepository _logradouroeRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;

        public ListarLogradouroHandler(ILogradouroRepository logradouroeRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _logradouroeRepository = logradouroeRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(ListarLogradouroQuery request, CancellationToken cancellationToken)
        {
            var response = new List<LogradouroResponse>();
            try
            {
                #region Validação do usuário autenticado

                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(false, "Acesso expirado");

                var usuarioLogado = await _usuarioRepository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(false, "Acesso expirado");

                #endregion

                var lista = await _logradouroeRepository.Listar(request.ClienteId, cancellationToken);
                response = LogradouroMapper<List<LogradouroResponse>>.Map(lista);

                return new ResultEvent(true, response, lista.Count());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}
