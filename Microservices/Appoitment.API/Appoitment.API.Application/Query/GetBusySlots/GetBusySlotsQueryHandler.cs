using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Query.GetBusySlots
{
    public class GetBusySlotsQueryHandler : IRequestHandler<GetBusySlotsQuery, BusyTimeSlotsDto[]>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetBusySlotsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<BusyTimeSlotsDto[]> Handle(GetBusySlotsQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetBusyTimeSlots(request.DoctorId, request.DateTime,
                cancellationToken);
        }
    }
}
