using System.ComponentModel.DataAnnotations;
using ThomasGreg.Domain.Interfaces;

namespace ThomasGreg.Domain.Models
{
    public class RefreshToken : IRefreshToken
    {
        [Key]
        public string Token { get; set; } = null!;
        public int UsuarioId { get; set; }
         public DateTime ExpirationDate { get; set; }
    }
}
