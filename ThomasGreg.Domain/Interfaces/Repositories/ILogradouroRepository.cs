using ThomasGreg.Domain.Models;

namespace ThomasGreg.Domain.Interfaces
{
    public interface ILogradouroRepository
    {
        Task<int> Salvar(Logradouro entity, CancellationToken cancellationToken);
        Task<ILogradouro> ObterPorId(int id,int clienteId, CancellationToken cancellationToken);
        Task<bool> ExcluirPorId(int id, int clienteId, CancellationToken cancellationToken);
        Task<IEnumerable<ILogradouro>?> Listar(int clienteId, CancellationToken cancellationToken);
    }
}
