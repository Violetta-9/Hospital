using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Command.Appointment.RescheduleAppointment;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Command.Appointment
{
    public class RescheduleAppointmentValidator:AbstractValidator<RescheduleAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        public RescheduleAppointmentValidator(IAppointmentRepository appointment,IDoctorRepository doctor)
        {
            _appointmentRepository = appointment;
            _doctorRepository = doctor;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.RescheduleAppointmentDto.DoctorId)
                .MustAsync(_doctorRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundDoctorId, opt.RescheduleAppointmentDto.DoctorId));

            RuleFor(x => x.RescheduleAppointmentDto.AppointmentId)
                .MustAsync(_appointmentRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundAppointmentId, opt.RescheduleAppointmentDto.AppointmentId));
        }
    }
}
