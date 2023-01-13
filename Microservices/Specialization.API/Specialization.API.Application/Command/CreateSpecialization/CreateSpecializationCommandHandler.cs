using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Specialization.API.Application.Services;
using SpecializationEntity= Authorization.Data_Domain.Models.Specialization;

namespace Specialization.API.Application.Command.CreateSpecialization
{
    internal class CreateSpecializationCommandHandler : IRequestHandler<CreateSpecializationCommand, long>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IServicesService _servicesService;
      
        

        public CreateSpecializationCommandHandler(ISpecializationRepository specializationRepository,IServicesService servicesService)
        {
            _specializationRepository = specializationRepository;
            _servicesService = servicesService;
         
            
        }

        public async Task<long> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
        {
            var specialization = new SpecializationEntity()
            {
                Title = request.CreateSpecializationDto.Title,
                IsActive = request.CreateSpecializationDto.IsActive,

            };

            
            await _specializationRepository.InsertAsync(specialization, cancellationToken);
            var serviceResponse = await _servicesService.SetSpecializationIdForServicesAsync(specialization.Id,
                request.CreateSpecializationDto.ServicesId, cancellationToken);

         if (!serviceResponse.IsSuccess)
         {
             throw new Exception("CreateSpecializationCommandHandler work bad");
         } 
         return specialization.Id;

        }
    }
}
