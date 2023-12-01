using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class ValidaUsuarioRefreshTokenQuery : Request<IEvent>
    {
        public string RefreshToken { get; }

        public ValidaUsuarioRefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
