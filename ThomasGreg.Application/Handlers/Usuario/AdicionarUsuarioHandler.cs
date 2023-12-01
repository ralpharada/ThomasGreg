using ThomasGreg.Application.Core;
using ThomasGreg.Application.Crypto;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Domain.Models;
using MediatR;

namespace ThomasGreg.Application.Handlers
{
    public class AdicionarUsuarioHandler : IRequestHandler<AdicionarUsuarioQuery, IEvent>
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioAutenticado _usuarioAutenticado;

        public AdicionarUsuarioHandler(IUsuarioRepository repository,
            UsuarioAutenticado usuarioAutenticado)
        {
            _repository = repository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<IEvent> Handle(AdicionarUsuarioQuery request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                if (String.IsNullOrWhiteSpace(request.Nome))
                    return new ResultEvent(success, "O campo Nome é obrigatório.");

                if (String.IsNullOrWhiteSpace(request.Email))
                    return new ResultEvent(success, "O campo Email é obrigatório.");

                if (String.IsNullOrWhiteSpace(request.Senha))
                    return new ResultEvent(success, "O campo Senha é obrigatório.");

                var existsUser = await _repository.ObterPorEmailCadastroAtivo(request.Email, cancellationToken);
                if (existsUser != null)
                    return new ResultEvent(success, "Jà existe um usuário com esse e-mail");

                var usuario = UsuarioMapper<Usuario>.Map(request);
                usuario.Senha = Criptografia.Encrypt(request.Senha);
                var upsertedId = await _repository.Salvar(usuario, cancellationToken);
                success = upsertedId > 0;
              
                return new ResultEvent(success, success ? "Usuário cadastrado com sucesso!" : "Falha ao cadastrar o usuário!", null, upsertedId);
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }

        }

    }
}
