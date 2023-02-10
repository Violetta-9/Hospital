using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Incoming;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.RescheduleAppointment
{
    public class RescheduleAppointmentCommand:IRequest<Response>
    {
        public RescheduleAppointmentDTO RescheduleAppointmentDto { get; set; }
        public RescheduleAppointmentCommand(RescheduleAppointmentDTO rescheduleAppointmentDto)
        {
            RescheduleAppointmentDto = rescheduleAppointmentDto;
        }
    }
}
