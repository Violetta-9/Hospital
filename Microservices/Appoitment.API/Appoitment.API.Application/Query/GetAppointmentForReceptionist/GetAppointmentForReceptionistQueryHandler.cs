using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Query.GetAppointmentForReceptionist
{
    internal class GetAppointmentForReceptionistQueryHandler : IRequestHandler<GetAppointmentForReceptionistQuery, AppointmentScheduleForReceptionistDTO[]>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentForReceptionistQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentScheduleForReceptionistDTO[]> Handle(GetAppointmentForReceptionistQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentRepository.GetAppointmentScheduleForReceptionistByCurrentDayAsync(request.Date,request.OfficeId,
                cancellationToken);
        }
    }
}
