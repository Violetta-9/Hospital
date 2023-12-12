using Appointment.API.Application.Contracts.Incoming;
using Appointment.API.Application.Contracts.Outgoing;
using MediatR;

namespace Appointment.API.Application.Command.Result.CreateAppointment
{
    public class CreateAppointmentResultCommand : IRequest<Response>
    {
        public CreateAppointmentResultDto CreateAppointmentResultDto { get; set; }

        public CreateAppointmentResultCommand(CreateAppointmentResultDto createAppointmentDto)
        {
            CreateAppointmentResultDto = createAppointmentDto;
        }
    }
}
