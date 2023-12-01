using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class SelecionarClienteQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public SelecionarClienteQuery(int id)
        {
            Id = id;
        }
    }
}
