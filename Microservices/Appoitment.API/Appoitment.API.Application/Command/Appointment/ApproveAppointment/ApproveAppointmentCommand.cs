using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.ApproveAppointment
{
    public class ApproveAppointmentCommand:IRequest<Response>
    {
        public long AppointmentId { get; set; }

        public ApproveAppointmentCommand(long appointmentId)
        {
            AppointmentId=appointmentId;
        }
    }
}
