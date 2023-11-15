using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetEmptyServices
{
    public class GetEmptyServicesQuery : IRequest<EmptyServices[]>
    {
    }
}
