using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using SpecializationEntity= Authorization.Data_Domain.Models.Specialization;

namespace Specialization.API.Application.Command.CreateSpecialization
{
    internal class CreateSpecializationCommandHandler : IRequestHandler<CreateSpecializationCommand, long>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IServiceRepository _serviceRepository;

        public CreateSpecializationCommandHandler(ISpecializationRepository specializationRepository, IServiceRepository serviceRepository)
        {
            _specializationRepository = specializationRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<long> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
        {
            var specialization = new SpecializationEntity()
            {
                Title = request.CreateSpecializationDto.Title,
                IsActive = request.CreateSpecializationDto.IsActive,

            };
            await _specializationRepository.InsertAsync(specialization, cancellationToken);

           await _serviceRepository.SetSpecializationAsync(request.CreateSpecializationDto.ServicesId, specialization.Id,
                cancellationToken);
           return specialization.Id;

        }
    }
}
