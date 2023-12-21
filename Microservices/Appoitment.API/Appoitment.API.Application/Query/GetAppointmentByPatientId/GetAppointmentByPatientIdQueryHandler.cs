using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Query.GetAppointmentByPatientId
{
    internal class GetAppointmentByPatientIdQueryHandler : IRequestHandler<GetAppointmentByPatientIdQuery, AppointmentHistoryDTO[]>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentByPatientIdQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<AppointmentHistoryDTO[]> Handle(GetAppointmentByPatientIdQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAppointmentsByPatientIdAsync(request.PatientId, cancellationToken);
        }
    }
}
