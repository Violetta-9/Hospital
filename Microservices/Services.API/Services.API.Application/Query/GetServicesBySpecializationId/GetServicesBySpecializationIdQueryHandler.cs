using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServicesBySpecializationId
{
    internal class GetServicesBySpecializationIdQueryHandler : IRequestHandler<GetServicesBySpecializationIdQuery, OutServicesDto[]>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetServicesBySpecializationIdQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public Task<OutServicesDto[]> Handle(GetServicesBySpecializationIdQuery request, CancellationToken cancellationToken)
        {
           return _serviceRepository.GetServiceBySpecializationIdAsync(request.SpecializationId, cancellationToken);
        }
    }
}
