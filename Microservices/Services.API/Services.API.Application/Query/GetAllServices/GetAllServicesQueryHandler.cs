using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetAllServices
{
    internal class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, OutServicesDto[]>
    {
        private readonly IServiceRepository _serviceRepository;
        public GetAllServicesQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<OutServicesDto[]> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
          return await _serviceRepository.GetAllServiceAsync(cancellationToken);
        }
    }
}
