using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Incoming;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.CreateAppointment
{
    public class CreateAppointmentCommand:IRequest<long>
    {
        public CreateAppointmentDTO AppointmentDto { get; set; }

        public CreateAppointmentCommand(CreateAppointmentDTO appointmentDto)
        {
            AppointmentDto=appointmentDto;
        }
    }
}
