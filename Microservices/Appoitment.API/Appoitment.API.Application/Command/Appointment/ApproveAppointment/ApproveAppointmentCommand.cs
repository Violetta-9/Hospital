using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.ApproveAppointment
{
    public class ApproveAppointmentCommand:IRequest<Response>
    {
        public long AppointmentId { get; set; }
        public bool IsApprove { get; set; }

        public ApproveAppointmentCommand(long appointmentId, bool isApprove)
        {
            AppointmentId = appointmentId;
            IsApprove = isApprove;
        }
    }
}
