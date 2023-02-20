
using MediatR;
using Profile.Application.Contracts.Outgoing;


namespace Profile.Application.Query.Status.GetStatus
{
    public class GetStatusQuery:IRequest<StatusDTO[]>
    {
    }
}
