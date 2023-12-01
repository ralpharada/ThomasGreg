using ThomasGreg.Domain.Interfaces;

namespace ThomasGreg.Domain.Models
{
    public partial class Arquivo : IArquivo
    {
        public string NomeArquivo { get; set; } = null!;
        public string Base64 { get; set; } = null!;
    }
}
