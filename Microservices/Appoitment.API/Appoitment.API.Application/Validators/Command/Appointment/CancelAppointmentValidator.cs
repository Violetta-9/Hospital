using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Command.Appointment.CancelAppointment;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Command.Appointment
{
    public class CancelAppointmentValidator:AbstractValidator<CancelAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CancelAppointmentValidator(IAppointmentRepository appointment)
        {
            _appointmentRepository = appointment;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.AppointmentId)
                .MustAsync(_appointmentRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundAppointmentId, opt.AppointmentId));
        }
    }
}
