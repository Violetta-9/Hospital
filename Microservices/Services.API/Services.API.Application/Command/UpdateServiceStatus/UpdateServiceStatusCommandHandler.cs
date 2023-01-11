using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Command.UpdateServiceStatus
{
    internal class UpdateServiceStatusCommandHandler : IRequestHandler<UpdateServiceStatusCommand, Response>
    {
        private readonly IServiceRepository _serviceRepository;

        public UpdateServiceStatusCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<Response> Handle(UpdateServiceStatusCommand request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetAsync(request.UpdateServiceStatusDTO.Id, cancellationToken);
            if (service == null)
            {
                return Response.Error;
            }
          
            service.IsActive = request.UpdateServiceStatusDTO.IsActive;
           
            await _serviceRepository.UpdateAsync(service, cancellationToken);
            return Response.Success;
        }
    }
}
