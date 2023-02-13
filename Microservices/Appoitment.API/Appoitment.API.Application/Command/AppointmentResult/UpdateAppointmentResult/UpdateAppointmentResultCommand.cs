using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Incoming;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Command.AppointmentResult.UpdateAppointmentResult
{
    public class UpdateAppointmentResultCommand:IRequest<Response>
    {
        public UpdateAppointmentResultDTO NewResult;

        public UpdateAppointmentResultCommand(UpdateAppointmentResultDTO newResult)
        {
            NewResult = newResult;
        }
    }
}
