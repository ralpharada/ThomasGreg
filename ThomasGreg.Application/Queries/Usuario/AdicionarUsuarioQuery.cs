using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class AdicionarUsuarioQuery : Request<IEvent>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public bool Status { get; private set; }
        public AdicionarUsuarioQuery( string nome, string email,bool status, string senha)
        {
            Nome = nome.Trim();
            Email = email.Trim();
            Status = status;
            Senha = senha.Trim();
        }
    }
}
