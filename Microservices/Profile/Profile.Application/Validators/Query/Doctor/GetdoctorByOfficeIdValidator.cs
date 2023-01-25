using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Doctor.GetDoctorByOfficeId;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Doctor;

internal class GetDoctorByOfficeIdValidator : AbstractValidator<GetDoctorsByOfficeIdQuery>
{
    private readonly IOfficeRepository _officeRepository;

    public GetDoctorByOfficeIdValidator(IOfficeRepository office)
    {
        _officeRepository = office;
        CreateRules();
    }

    private void CreateRules()
    {

        RuleFor(x => x.OfficeId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsOfficeAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.OfficeId));
    }

    private async Task<bool> ExistsOfficeAsync(long officeId, CancellationToken cancellationToken)
    {
        return await _officeRepository.ExistsAsync(officeId, cancellationToken);
    }
}