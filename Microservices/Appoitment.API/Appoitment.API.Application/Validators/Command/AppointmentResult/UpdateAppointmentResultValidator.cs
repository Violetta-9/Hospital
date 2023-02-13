using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Command.AppointmentResult.UpdateAppointmentResult;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Command.AppointmentResult
{
    public class UpdateAppointmentResultValidator:AbstractValidator<UpdateAppointmentResultCommand>
    {
        private readonly IResultRepository _resultRepository;

        public UpdateAppointmentResultValidator(IResultRepository resultRepository)
        {
            _resultRepository= resultRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.NewResult.AppointmentResultId)
                .MustAsync(_resultRepository.ExistsAsync)
                .WithMessage(opt =>
                    string.Format(Messages.NotFoundAppointmentResultId, opt.NewResult.AppointmentResultId));
        }
    }
}
