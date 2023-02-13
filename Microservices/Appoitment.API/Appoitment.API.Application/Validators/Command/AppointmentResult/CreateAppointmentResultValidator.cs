using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Command.AppointmentResult.CreateAppointmentResult;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Command.AppointmentResult
{
    public class CreateAppointmentResultValidator : AbstractValidator<CreateAppointmentResultCommand>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CreateAppointmentResultValidator(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.CreateAppointmentDTO.AppointmentId)
                .MustAsync(_appointmentRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundAppointmentId, opt.CreateAppointmentDTO.AppointmentId));
        }
    }
}
