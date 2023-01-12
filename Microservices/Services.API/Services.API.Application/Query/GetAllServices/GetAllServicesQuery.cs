using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetAllServices;

public class GetAllServicesQuery : IRequest<OutServicesDto[]>
{
}