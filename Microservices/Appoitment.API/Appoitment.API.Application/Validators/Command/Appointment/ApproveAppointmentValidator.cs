using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Command.Appointment.ApproveAppointment;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Command.Appointment
{
    public class ApproveAppointmentValidator:AbstractValidator<ApproveAppointmentCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ApproveAppointmentValidator(IAppointmentRepository appointment)
        {
            _appointmentRepository= appointment;
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
