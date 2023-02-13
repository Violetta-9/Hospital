using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Incoming;
using MediatR;

namespace Appointment.API.Application.Command.AppointmentResult.CreateAppointmentResult
{
    public class CreateAppointmentResultCommand:IRequest<long>
    {
        public CreateAppointmentResultDTO CreateAppointmentDTO { get; set; }
        public CreateAppointmentResultCommand(CreateAppointmentResultDTO createAppointmentDTO)
        {
            CreateAppointmentDTO = createAppointmentDTO;
        }
    }
}
