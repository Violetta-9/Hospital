using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.API.Contracts.Outgoing;
using Authorization.Data.Repository;

namespace Services.API.Application.Command.UpdateSpecializationForServices
{
    internal class UpdateSpecializationForServiceCommandHandler : IRequestHandler<UpdateSpecializationForServiceCommand, Response>
    {
        private readonly IServiceRepository _serviceRepository;

        public UpdateSpecializationForServiceCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository=serviceRepository;
        }
        public async Task<Response> Handle(UpdateSpecializationForServiceCommand request, CancellationToken cancellationToken)
        {
           await _serviceRepository.UpdateSpecializationIdAsync(request.ServicesId, request.SpecializationId,
                cancellationToken);
            return Response.Success;

        }
    }
}
