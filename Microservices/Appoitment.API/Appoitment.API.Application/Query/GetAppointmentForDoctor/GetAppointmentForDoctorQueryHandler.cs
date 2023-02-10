using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Query.GetAppointmentForDoctor
{
    internal class GetAppointmentForDoctorQueryHandler : IRequestHandler<GetAppointmentForDoctorQuery, AppointmentScheduleForDoctorDTO[]>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentForDoctorQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<AppointmentScheduleForDoctorDTO[]> Handle(GetAppointmentForDoctorQuery request, CancellationToken cancellationToken)
        {
           return await _appointmentRepository.GetAppointmentScheduleForDoctorByCurrentDayAsync(request.Date,request.DoctorId, cancellationToken);
        }
    }
}
