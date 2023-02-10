using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.RescheduleAppointment
{
    internal class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand, Response>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public RescheduleAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Response> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(request.RescheduleAppointmentDto.AppointmentId, cancellationToken);
            if (appointment != null)
            {
                appointment.DoctorId = request.RescheduleAppointmentDto.DoctorId;
                appointment.DateTime = request.RescheduleAppointmentDto.DataTime;
                await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
                return Response.Success;
            }
            return Response.Error;
        }
    }
}
