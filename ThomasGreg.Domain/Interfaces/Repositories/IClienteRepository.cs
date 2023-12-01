using ThomasGreg.Domain.Models;

namespace ThomasGreg.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<int> Salvar(Cliente entity, CancellationToken cancellationToken);
        Task<ICliente> ObterPorId(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ICliente>> Listar(CancellationToken cancellationToken);
        Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken);
        Task<bool> ExisteEmailCadastrado(string email, int id, CancellationToken cancellationToken);
    }
}
