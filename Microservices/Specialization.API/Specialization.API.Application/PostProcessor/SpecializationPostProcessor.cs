using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data_Domain.Models;
using MassTransit;
using MediatR.Pipeline;
using Specialization.API.Application.Command.UpdateSpecializationStatus;
using Specialization.API.Application.Contracts.Outgoing;
using SpecializationStatusChanged;
using Response = Specialization.API.Application.Contracts.Outgoing.Response;

namespace Specialization.API.Application.PostProcessor
{
    public class SpecializationPostProcessor : IRequestPostProcessor<UpdateSpecializationStatusCommand, Response>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public SpecializationPostProcessor(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Process(UpdateSpecializationStatusCommand request, Response response, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish<SpecializationStatusChanged.SpecializationStatusChanged>(new
            {
                SpecializationId=request.UpdateSpecializationStatusDto.Id,
                IsActive = request.UpdateSpecializationStatusDto.IsActive
            }, cancellationToken);
        }
    }
}
