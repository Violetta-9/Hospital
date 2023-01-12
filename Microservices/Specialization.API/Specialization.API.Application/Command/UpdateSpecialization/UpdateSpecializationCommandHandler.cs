using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Command.UpdateSpecialization
{
    public class UpdateSpecializationCommandHandler : IRequestHandler<UpdateSpecializationCommand, Response>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IServiceRepository _serviceRepository;

        public UpdateSpecializationCommandHandler(IServiceRepository serviceRepository,
            ISpecializationRepository specializationRepository)
        {
            _serviceRepository= serviceRepository;
            _specializationRepository= specializationRepository;
        }
        public async Task<Response> Handle(UpdateSpecializationCommand request, CancellationToken cancellationToken)
        {
           var specialization=await _specializationRepository.GetAsync(request.Id, cancellationToken);
           if (specialization == null)
           {
                return Response.Error;
           }
           specialization.Title=request.Title;
          await _specializationRepository.UpdateAsync(specialization, cancellationToken);
         await _serviceRepository.SetSpecializationAsync(request.ServicesId, specialization.Id, cancellationToken);
         return Response.Success;
        }
    }
}
