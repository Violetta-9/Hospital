using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MediatR.Pipeline;
using Office.Application.Command.UpdateOfficeStatus;
using Office.Application.Contracts.Outgoing;
using OfficeStatusChanged;
using Response = Office.Application.Contracts.Outgoing.Response;

namespace Office.Application.PostProcessor
{
    public class OfficePostProcessor : IRequestPostProcessor<UpdateOfficeStatusCommand, Response>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public OfficePostProcessor(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Process(UpdateOfficeStatusCommand request, Response response, CancellationToken cancellationToken)
        {
           
            
                await _publishEndpoint.Publish<OfficeStatusChanged.OfficeStatusChanged>(new
                {
                    OfficeId = request.OfficeId,
                    IsActive=request.IsActive
                },cancellationToken);
                
            
            return;
        }
    }
}
