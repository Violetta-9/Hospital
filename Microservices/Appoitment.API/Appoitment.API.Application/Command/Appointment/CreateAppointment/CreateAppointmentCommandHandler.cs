using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using AppointmentEntity= Authorization.Data_Domain.Models.Appointment;

namespace Appointment.API.Application.Command.Appointment.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, long>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository= appointmentRepository;
        }

        public async Task<long> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new AppointmentEntity()
            {
                DoctorId = request.AppointmentDto.DoctorId,
                PatientId = request.AppointmentDto.PatientId,
               // ServiceId = request.AppointmentDto.ServiceId,
                DateTime = request.AppointmentDto.DateTime,
                OfficeId = request.AppointmentDto.OfficeId,
                SpecializationId = request.AppointmentDto.SpecializationId,
                IsApproved = false
            };
           await _appointmentRepository.InsertAsync(appointment,cancellationToken);

           return appointment.Id;
        }
    }
}
