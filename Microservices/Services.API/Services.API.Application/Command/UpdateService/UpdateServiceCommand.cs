using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.API.Contracts.Incoming;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Command.UpdateService
{
    public class UpdateServiceCommand:IRequest<Response>
    {
       public UpdateServiceDTO UpdateServiceDto { get; set; }

       public UpdateServiceCommand(UpdateServiceDTO updateServiceDto)
       {
           UpdateServiceDto = updateServiceDto;
       }
    }
}
