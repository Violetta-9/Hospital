using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Specialization.API.Application.Contracts.Outgoing;
using Specialization.API.Application.Services;

namespace Specialization.API.Application.Query.GetSpecializationById
{
    public class GetSpecializationByIdQueryHandler : IRequestHandler<GetSpecializationByIdQuery, SpecializationDTO>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IServicesService _servicesService;
        public GetSpecializationByIdQueryHandler(ISpecializationRepository repository,IServicesService servicesService)
        {
            _specializationRepository = repository;
            _servicesService = servicesService;
        }

        public async Task<SpecializationDTO> Handle(GetSpecializationByIdQuery request, CancellationToken cancellationToken)
        {
         var specialization= await _specializationRepository.GetSpecializationByIdAsync(request.SpecializationId, cancellationToken);
         specialization.Services= await _servicesService.GetServicesBySpecializationId(specialization.Id, cancellationToken);
         return specialization;
        }
    }
}
