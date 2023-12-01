using ThomasGreg.Application.Core;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Application.Responses;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;

namespace ThomasGreg.Application.Handlers
{
    public class ListarUsuarioHandler : IRequestHandler<ListarUsuarioQuery, IEvent>
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public ListarUsuarioHandler(IUsuarioRepository repository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _repository = repository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(ListarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var response = new List<UsuarioResponse>();
            try
            {
                if (_usuarioAutenticado.Email == null)
                    return new ResultEvent(false, "Acesso expirado");

                var usuarioLogado = await _repository.ObterPorEmailCadastroAtivo(_usuarioAutenticado.Email, cancellationToken);
                if (usuarioLogado == null)
                    return new ResultEvent(false, "Acesso expirado");

                var lista = await _repository.Listar(cancellationToken);
                response = UsuarioMapper<List<UsuarioResponse>>.Map(lista);

                return new ResultEvent(true, response, lista.Count());
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
        }
    }
}
