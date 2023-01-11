using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServicesById;

internal class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, OutServicesDto>
{
    private readonly IServiceRepository _serviceRepository;

    public GetServiceByIdQueryHandler(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }

    public async Task<OutServicesDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        return await _serviceRepository.GetServiceByIdAsync(request.Id, cancellationToken);
    }
}