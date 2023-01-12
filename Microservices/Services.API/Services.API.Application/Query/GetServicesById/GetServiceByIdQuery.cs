using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServicesById;

public class GetServiceByIdQuery : IRequest<OutServicesDto>
{
    public long Id { get; set; }

    public GetServiceByIdQuery(long id)
    {
        Id = id;
    }
}