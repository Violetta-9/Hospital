using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServiceBySpecializationId
{
    internal class GetServiceBySpecializationIdQueryHandler : IRequestHandler<GetServiceBySpecializationIdQuery, OutServicesDto[]>
    {

        private readonly IServiceRepository _serviceRepository;

        public GetServiceBySpecializationIdQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<OutServicesDto[]> Handle(GetServiceBySpecializationIdQuery request, CancellationToken cancellationToken)
        {
            return await _serviceRepository.GetServiceBySpecializationIdAsync(request.SpecializationId,
                cancellationToken);
        }
    }
}
