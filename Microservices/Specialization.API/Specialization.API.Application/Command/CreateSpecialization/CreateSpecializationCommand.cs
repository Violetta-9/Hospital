using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Specialization.API.Application.Contracts.Incoming;

namespace Specialization.API.Application.Command.CreateSpecialization
{
    public class CreateSpecializationCommand:IRequest<long>
    {
        public CreateSpecializationDTO CreateSpecializationDto { get; set; }

        public CreateSpecializationCommand(CreateSpecializationDTO createSpecialization)
        {
            CreateSpecializationDto=createSpecialization;
        }

    }
}
