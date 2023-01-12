using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using Services.API.Contracts.Incoming;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Command.SetSpecializationForService
{
    public class SetSpecializationCommand:IRequest<Response>
    {
        public SetSpecializationDTO SetSpecializationDTO { get; set; }

        public SetSpecializationCommand(SetSpecializationDTO setSpecializationDTO)
        {
            SetSpecializationDTO = setSpecializationDTO;
        }
    }
}
