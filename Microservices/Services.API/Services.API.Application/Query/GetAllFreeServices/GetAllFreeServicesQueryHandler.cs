using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetAllFreeServices
{
    internal class GetAllFreeServicesQueryHandler : IRequestHandler<GetAllFreeServicesQuery, OutServicesDto[]>
    {
        private readonly IServiceRepository _serviceRepository;

        public GetAllFreeServicesQueryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<OutServicesDto[]> Handle(GetAllFreeServicesQuery request, CancellationToken cancellationToken)
        {
         return  await _serviceRepository.GetAllFreeServiceAsync(cancellationToken);
        }
    }
}
