using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.RescheduleAppointment
{
    internal class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand, AppointmentHistoryDTO[]>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public RescheduleAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<AppointmentHistoryDTO[]> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(request.RescheduleAppointmentDto.AppointmentId, cancellationToken);
            if (appointment != null)
            {
                appointment.DoctorId = request.RescheduleAppointmentDto.DoctorId;
                appointment.DateTime = request.RescheduleAppointmentDto.DataTime;
                appointment.IsApproved = false;
                await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
            }

            return await _appointmentRepository.GetAppointmentsByPatientIdAsync(
                request.RescheduleAppointmentDto.PatientId, cancellationToken);
        }
    }
}
