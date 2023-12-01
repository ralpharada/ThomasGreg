using Microsoft.EntityFrameworkCore;
using ThomasGreg.Infra.Context;

namespace ThomasGreg.Infra.DataAccess
{
    public static class InicializadorBancoDados
    {
        public static void Inicializar(AppDbContext contexto)
        {
            if (!contexto.Database.CanConnect())
            {
                contexto.Database.Migrate();

                var path = "./DataBase/";

                // Execute o script principal
                string scriptSql = File.ReadAllText($"{path}script.sql");
                contexto.Database.ExecuteSqlRaw(scriptSql);

                // Recupera todas as procedures da pasta e migra para o banco
                DirectoryInfo di = new DirectoryInfo($"{path}procedures");

                foreach (var item in di.GetFiles())
                {
                    scriptSql = File.ReadAllText(item.FullName);
                    if (!String.IsNullOrWhiteSpace(scriptSql))
                        contexto.Database.ExecuteSqlRaw(scriptSql);
                }
            }
        }
    }
}
