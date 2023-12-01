using ThomasGreg.Domain.Interfaces;
using ThomasGreg.Domain.Models;
using ThomasGreg.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ThomasGreg.Infra.Repositories
{
    public class LogradouroRepository : ILogradouroRepository
    {
        private readonly AppDbContext _context;
        public LogradouroRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Salvar(Logradouro entity, CancellationToken cancellationToken)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
               {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@ClienteId", entity.ClienteId),
                new SqlParameter("@NomeRua", entity.NomeRua),
                new SqlParameter("@Numero", entity.Numero),
                new SqlParameter("@Bairro", entity.Bairro),
                new SqlParameter("@Cidade", entity.Cidade),
                new SqlParameter("@Estado", entity.Estado),
                new SqlParameter("@Cep", entity.Cep),
                new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
               };

                await _context.Database.ExecuteSqlRawAsync("EXEC InserirAtualizarLogradouro @Id, @ClienteId, @NomeRua, @Numero, @Bairro, @Cidade, @Estado, @Cep, @RowCount OUTPUT", parameters, cancellationToken);
                return (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;
            }
            catch (Exception ex)
            {
                // Tratar exceção conforme necessário
                Console.Error.WriteLine($"Erro ao excluir logradouro: {ex.Message}");
                return 0;
            }
        }
        public async Task<ILogradouro> ObterPorId(int id, int clienteId, CancellationToken cancellationToken)
        {
            return await _context.Logradouros.FirstOrDefaultAsync(x => x.Id == id && x.ClienteId == clienteId, cancellationToken);
        }
        public async Task<bool> ExcluirPorId(int id, int clienteId, CancellationToken cancellationToken)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@ClienteId", clienteId),
                    new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC ExcluirLogradouro @Id, @ClienteId, @RowCount OUTPUT", parameters, cancellationToken);
                int rowCount = (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;
                return rowCount > 0;
            }
            catch (Exception ex)
            {
                // Tratar exceção conforme necessário
                Console.Error.WriteLine($"Erro ao excluir logradouro: {ex.Message}");
                return false;
            }
        }
        public async Task<IEnumerable<ILogradouro>?> Listar(int clienteId, CancellationToken cancellationToken)
        {
            try
            {
                return await _context.Logradouros.Where(x => x.ClienteId == clienteId).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // Tratar exceção conforme necessário
                Console.Error.WriteLine($"Erro ao excluir logradouro: {ex.Message}");
                return null;
            }
        }
    }
}
