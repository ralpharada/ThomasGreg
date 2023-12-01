using ThomasGreg.Domain.Interfaces;

namespace ThomasGreg.Domain.Models
{
    public partial class Logradouro: ILogradouro
    {
        public int Id { get; set; }
        public string NomeRua { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string Cep { get; set; } = null!;

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
    }
}
