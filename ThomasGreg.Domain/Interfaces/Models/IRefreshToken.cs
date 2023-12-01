
using System.ComponentModel.DataAnnotations;

namespace ThomasGreg.Domain.Interfaces
{
    public interface IRefreshToken
    {
        [Key]
        string Token { get; set; }
        int UsuarioId { get; set; }
        DateTime ExpirationDate { get; set; }
    }
}
