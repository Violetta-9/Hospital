using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<OutServicesDto[]> Handle(GetServicesBySpecializationIdQuery request, CancellationToken cancellationToken)
        {
             return await _serviceRepository.GetServiceBySpecializationIdAsync(request.SpecializationId, cancellationToken);
        }
    }
}
