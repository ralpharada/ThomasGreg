using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class SelecionarLogradouroQuery : Request<IEvent>
    {
        public int Id { get; private set; }
        public int ClienteId { get; private set; }
        public SelecionarLogradouroQuery(int id,int clienteId)
        {
            Id = id;
            ClienteId = clienteId;
        }
    }
}
