using ThomasGreg.Domain.Models;

namespace ThomasGreg.Domain.Interfaces
{
    public interface ICliente
    {
        int Id { get; set; }
        string Nome { get; set; }
        List<Logradouro> Logradouros { get; set; }
        string Email { get; set; } 
        string Logotipo { get; set; } 
    }
}
