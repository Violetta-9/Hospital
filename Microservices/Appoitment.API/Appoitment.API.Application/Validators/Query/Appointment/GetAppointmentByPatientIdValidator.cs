using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Query.GetAppointmentByPatientId;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Query.Appointment
{
    public class GetAppointmentByPatientIdValidator:AbstractValidator<GetAppointmentByPatientIdQuery>
    {
        private readonly IPatientRepository _patientRepository;
        public GetAppointmentByPatientIdValidator(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.PatientId)
                .MustAsync(_patientRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundPatientId, opt.PatientId));
        }
    }
}
