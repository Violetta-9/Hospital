using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Services.API.Client.Abstraction;

using SpecializationEntity= Authorization.Data_Domain.Models.Specialization;

namespace Specialization.API.Application.Command.CreateSpecialization
{
    internal class CreateSpecializationCommandHandler : IRequestHandler<CreateSpecializationCommand, long>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IServiceApiProxy _serviceApiProxy;

        public CreateSpecializationCommandHandler(ISpecializationRepository specializationRepository,IServiceApiProxy serviceApiProxy)
        {
            _specializationRepository = specializationRepository;
            _serviceApiProxy = serviceApiProxy;
         
            
        }

        public async Task<long> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
        {
            var specialization = new SpecializationEntity()
            {
                Title = request.CreateSpecializationDto.Title,
                IsActive = request.CreateSpecializationDto.IsActive
            };

            
            await _specializationRepository.InsertAsync(specialization, cancellationToken);
            try
            {
                var serviceResponse = await _serviceApiProxy.SetSpecializationIdForServicesAsync(specialization.Id,
                    request.CreateSpecializationDto.ServicesId, cancellationToken);
                if (!serviceResponse.IsSuccess)
                {
                    throw new Exception("CreateSpecializationCommandHandler work bad");

                }
            }
            catch (ValidationException e)
            {
                await _specializationRepository.DeleteAsync(specialization.Id, cancellationToken);
                throw e;
            }
            catch (Exception e)
            {
                await _specializationRepository.DeleteAsync(specialization.Id, cancellationToken);
                throw e;
            }

            return specialization.Id;

        }
    }
}
