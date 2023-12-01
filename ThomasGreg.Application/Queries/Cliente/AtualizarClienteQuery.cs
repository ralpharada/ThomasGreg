using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Queries
{
    public class AtualizarClienteQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Arquivo Logotipo { get; private set; }
        public AtualizarClienteQuery(int id, string nome, string email, Arquivo logotipo)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Logotipo = logotipo;
        }
    }
}
