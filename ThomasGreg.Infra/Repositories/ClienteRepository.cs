using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ThomasGreg.Infra.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(Cliente entity, CancellationToken cancellationToken)
        {
            SqlParameter[] parameters = new SqlParameter[]
               {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Nome", entity.Nome),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Logotipo", entity.Logotipo),
                new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
               };

            await _context.Database.ExecuteSqlRawAsync("EXEC InserirAtualizarCliente @Id, @Nome, @Email, @Logotipo, @RowCount OUTPUT", parameters, cancellationToken);
            return (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;

        }
        public async Task<ICliente> ObterPorId(int id, CancellationToken cancellationToken)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<ICliente>> Listar(CancellationToken cancellationToken)
        {
            return await _context.Clientes.ToListAsync(cancellationToken);
        }

        public async Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC ExcluirCliente @Id, @RowCount OUTPUT", parameters, cancellationToken);
                int rowCount = (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;
                return rowCount > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ExisteEmailCadastrado(string email, int id, CancellationToken cancellationToken)
        {
            return await _context.Clientes.AnyAsync(x => x.Id != id && x.Email == email, cancellationToken);
        }
    }
}
