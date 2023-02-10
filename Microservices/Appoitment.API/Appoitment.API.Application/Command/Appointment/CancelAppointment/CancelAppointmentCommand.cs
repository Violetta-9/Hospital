using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.CancelAppointment
{
    public class CancelAppointmentCommand:IRequest<Response>
    {
        public long AppointmentId { get; set; }

        public CancelAppointmentCommand(long appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
