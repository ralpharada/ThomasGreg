using ThomasGreg.Domain.Models;

namespace ThomasGreg.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> Salvar(Usuario entity, CancellationToken cancellationToken);
        Task<IUsuario> ObterPorEmailCadastroAtivo(string email, CancellationToken cancellationToken);
        Task<IUsuario> ObterPorId(int id, CancellationToken cancellationToken); 
        Task<IEnumerable<IUsuario>> Listar(CancellationToken cancellationToken);
    }

}
