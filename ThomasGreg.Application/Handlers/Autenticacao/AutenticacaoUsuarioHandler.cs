using ThomasGreg.Application.Contracts;
using ThomasGreg.Application.Crypto;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Handlers.Autenticacao
{
    public class AutenticacaoUsuarioHandler : IRequestHandler<AutenticacaoUsuarioQuery, IEvent>
    {
        private readonly IJwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public AutenticacaoUsuarioHandler(
            IJwtService jwtService,
            IUsuarioRepository usuariorRepository,
            IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor,
        IMediator mediator)
        {
            _jwtService = jwtService;
            _usuarioRepository = usuariorRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public async Task<IEvent> Handle(AutenticacaoUsuarioQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IUsuario usuario = await _usuarioRepository.ObterPorEmailCadastroAtivo(request.Email, cancellationToken);
                if (usuario == null)
                {
                    return new ResultEvent(false, "Verifique se digitou corretamente os dados de acesso e tente novamente.");
                }
                if (!Criptografia.Verify(request.Password, usuario.Senha))
                {
                    return new ResultEvent(false, "Login/Senha inválidos, tente novamente.");
                }
                var jwt = _jwtService.GenerateUsuarioToken(usuario);
                await _refreshTokenRepository.AtualizarPorUsuarioId(jwt.RefreshToken);
                var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                return new ResultEvent(true, new JwtToken
                {
                    AccessToken = jwt.AccessToken,
                    Token = jwt.RefreshToken.Token,
                    TokenType = jwt.TokenType,
                    ExpiresIn = jwt.ExpiresIn
                });
            }
            catch (Exception ex)
            {
                return new ResultEvent(false, ex.Message);
            }
           
        }
    }
}
