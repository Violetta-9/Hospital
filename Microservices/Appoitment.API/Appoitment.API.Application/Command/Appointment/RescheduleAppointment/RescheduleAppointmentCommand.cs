using Appointment.API.Application.Contracts.Incoming;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.RescheduleAppointment
{
    public class RescheduleAppointmentCommand:IRequest<AppointmentHistoryDTO[]>
    {
        public RescheduleAppointmentDTO RescheduleAppointmentDto { get; set; }
        public RescheduleAppointmentCommand(RescheduleAppointmentDTO rescheduleAppointmentDto)
        {
            RescheduleAppointmentDto = rescheduleAppointmentDto;
        }
    }
}
