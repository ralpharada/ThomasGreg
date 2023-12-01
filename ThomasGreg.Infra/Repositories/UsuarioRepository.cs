using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ThomasGreg.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(Usuario entity, CancellationToken cancellationToken)
        {
            var usuario = await _context.Usuarios.FindAsync(entity.Id);

            if (usuario != null)
            {
                _context.Entry(usuario).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Usuarios.Add((Usuario)entity);
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IUsuario> ObterPorEmailCadastroAtivo(string email, CancellationToken cancellationToken)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email , cancellationToken);
        }
        public async Task<IUsuario> ObterPorId(int id, CancellationToken cancellationToken)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<IUsuario>> Listar( CancellationToken cancellationToken)
        {
            return await _context.Usuarios.ToListAsync(cancellationToken);
        }
    }
}
