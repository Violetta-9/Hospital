using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Status
{
    public class GetAllStatusQuery : IRequest<StatusAllDto[]>
    {
    }
}
