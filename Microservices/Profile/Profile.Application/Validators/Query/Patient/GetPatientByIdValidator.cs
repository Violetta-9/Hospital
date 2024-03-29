﻿using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Patient.GetPatientById;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Patient;

internal class GetPatientByIdValidator : AbstractValidator<GetPatientByIdQuery>
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
            .MustAsync(ExistsPatient)
            .WithMessage(opt => string.Format(Messages.NotFoundPatient, opt.PatientId));
    }

    private async Task<bool> ExistsPatient(long patientId, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId, cancellationToken);
        return patient != null;
    }
}