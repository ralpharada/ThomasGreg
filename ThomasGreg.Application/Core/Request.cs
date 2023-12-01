using MediatR;

namespace ThomasGreg.Application.Core
{
    public abstract class Request<TResponse> :  IRequest<TResponse>
    {

    }
}
