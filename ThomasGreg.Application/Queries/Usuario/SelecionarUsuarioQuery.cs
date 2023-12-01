using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class SelecionarUsuarioQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public SelecionarUsuarioQuery(int id)
        {
            Id = id;
        }
    }
}
