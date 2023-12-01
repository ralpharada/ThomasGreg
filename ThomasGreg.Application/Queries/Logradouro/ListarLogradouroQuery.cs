using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class ListarLogradouroQuery : Request<IEvent>
    {
        public int ClienteId { get;private set; }
        public ListarLogradouroQuery(int clienteId)
        {
            ClienteId = clienteId;
        }
    }
}
