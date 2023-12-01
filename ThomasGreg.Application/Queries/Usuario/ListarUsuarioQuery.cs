using ThomasGreg.Application.Core;
using ThomasGreg.Core.Events;

namespace ThomasGreg.Application.Queries
{
    public class ListarUsuarioQuery : Request<IEvent>
    {
        public ListarUsuarioQuery()
        {
        }
    }
}
