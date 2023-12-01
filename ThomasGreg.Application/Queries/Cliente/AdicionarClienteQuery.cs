using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Queries
{
    public class AdicionarClienteQuery : Request<IEvent>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Arquivo Logotipo { get; private set; }
        public AdicionarClienteQuery(string nome, string email, Arquivo logotipo)
        {
            Nome = nome;
            Email = email;
            Logotipo = logotipo;
        }
    }

}
