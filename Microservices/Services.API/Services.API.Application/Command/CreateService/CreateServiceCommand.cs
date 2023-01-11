using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.API.Contracts.Incoming;

namespace Services.API.Application.Command.CreateService
{
    public class CreateServiceCommand:IRequest<long>
    {
    public CreateServiceDTO CreateServiceDto { get; set; }
    public CreateServiceCommand(CreateServiceDTO createServiceDto)
        {
            CreateServiceDto = createServiceDto;
        }
    }
}
