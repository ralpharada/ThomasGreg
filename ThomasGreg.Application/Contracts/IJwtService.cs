using ThomasGreg.Domain.Interfaces;
namespace ThomasGreg.Application.Contracts
{
    public interface IJwtService
    {
        IJsonWebToken GenerateUsuarioToken(IUsuario user);
    }
}
