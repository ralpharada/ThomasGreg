using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class ListarClienteQuery : Request<IEvent>
    {
        public ListarClienteQuery()
        {
        }
    }
}
