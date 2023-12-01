using ThomasGreg.Application.Contracts;
using ThomasGreg.Application.Queries;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Handlers.Autenticacao
{
    public class ValidaUsuarioRefreshTokenHandler : IRequestHandler<ValidaUsuarioRefreshTokenQuery, IEvent>
    {
        private readonly IJwtService _jwtService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public ValidaUsuarioRefreshTokenHandler(
            IJwtService jwtService,
            IUsuarioRepository usuarioRepository,
            IRefreshTokenRepository refreshTokenRepository,
        IHttpContextAccessor httpContextAccessor,
        IMediator mediator)
        {
            _jwtService = jwtService;
            _usuarioRepository = usuarioRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public async Task<IEvent> Handle(ValidaUsuarioRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IUsuario usuario = null;
                if (!String.IsNullOrEmpty(request.RefreshToken))
                {
                    var token = await _refreshTokenRepository.ObterPorChaveUsuario(request.RefreshToken);
                    if (token == null || token.ExpirationDate < DateTime.Now)
                        return new ResultEvent(false, new JwtToken
                        {
                            AccessToken = string.Empty,
                            Token = string.Empty,
                            TokenType = string.Empty,
                            ExpiresIn = 0
                        });
                    usuario = await _usuarioRepository.ObterPorId(token.UsuarioId, new CancellationToken());
                }
                if (usuario == null)
                    return new ResultEvent(false, new JwtToken
                    {
                        AccessToken = string.Empty,
                        Token = string.Empty,
                        TokenType = string.Empty,
                        ExpiresIn = 0
                    });

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
