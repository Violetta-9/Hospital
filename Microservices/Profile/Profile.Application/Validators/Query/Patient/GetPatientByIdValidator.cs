using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Patient.GetPatientById;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Patient
{
    internal class GetPatientByIdValidator:AbstractValidator<GetPatientByIdQuery>
    {
        private readonly IPatientRepository _patientRepository;
        public GetPatientByIdValidator(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.PatientId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(opt => string.Format(Messages.EmptyField, nameof(opt.PatientId)))
                .MustAsync(ExistsPatient)
                .WithMessage(opt=>string.Format(Messages.NotFoundPatient,opt.PatientId));
        }

        private async Task<bool> ExistsPatient(long patientId, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetPatientByIdAsync(patientId, cancellationToken);
            return patient != null;
        }
    }
}
