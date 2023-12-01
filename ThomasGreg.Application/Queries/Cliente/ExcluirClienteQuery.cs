using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class ExcluirClienteQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public ExcluirClienteQuery(int id)
        {
            Id = id;
        }
    }
}
