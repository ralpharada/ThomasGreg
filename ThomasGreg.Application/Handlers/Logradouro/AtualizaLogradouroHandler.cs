using ThomasGreg.Application.Core;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;
using ThomasGreg.Application.Mapper;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Handlers
{
    public class AtualizarLogradouroHandler : IRequestHandler<AtualizarLogradouroQuery, IEvent>
    {
        private readonly ILogradouroRepository _logradouroRepository;
        private readonly UsuarioAutenticado _usuarioAutenticado;
        private readonly IUsuarioRepository _usuarioRepository;
        public AtualizarLogradouroHandler(ILogradouroRepository logradouroRepository,
            UsuarioAutenticado usuarioAutenticado,
            IUsuarioRepository usuarioRepository)
        {
            _logradouroRepository = logradouroRepository;
            _usuarioAutenticado = usuarioAutenticado;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEvent> Handle(AtualizarLogradouroQuery request, CancellationToken cancellationToken)
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

                var logradouro = await _logradouroRepository.ObterPorId(request.Id, request.ClienteId, cancellationToken);

                if (logradouro == null)
                    return new ResultEvent(success, "Logradouro não localizado.");
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

                logradouro.NomeRua = request.NomeRua;
                logradouro.Numero = request.Numero;
                logradouro.Bairro = request.Bairro;
                logradouro.Cidade = request.Cidade;
                logradouro.Estado = request.Estado;
                logradouro.Cep = request.Cep;

                var logradouroMap = LogradouroMapper<Logradouro>.Map(logradouro);
                var result = await _logradouroRepository.Salvar(logradouroMap, cancellationToken);
                success = result > 0;

                return new ResultEvent(success, success ? "Logradouro atualizado com sucesso!" : "Nenhuma atualização realizada.");
            }
            catch (Exception ex)
            {
                return new ResultEvent(success, ex.Message);
            }
        }

    }
}
