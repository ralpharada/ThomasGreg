using ThomasGreg.Domain.Models;

namespace ThomasGreg.Domain.Interfaces
{
    public interface IJsonWebToken
    {
        string AccessToken { get; set; }
        RefreshToken RefreshToken { get; set; }
        string TokenType { get; set; }
        long ExpiresIn { get; set; }
    }

}
