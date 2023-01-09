using FluentValidation;
using Profile.Application.Query.Doctor.GetDoctorByOfficeId;

namespace Profile.Application.Validators.Query.Doctor;

internal class GetDoctorByOfficeIdValidator : AbstractValidator<GetDoctorsByOfficeIdQuery>
{
    public GetDoctorByOfficeIdValidator()
    {
        CreateRules();
    }

    private void CreateRules()
    {
        //todo: validator
        //RuleFor(x => x.OfficeId)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .WithMessage(opt => string.Format(Messages.EmptyField, nameof(opt.OfficeId)))
        //    .MustAsync(ExistsOffice)
        //    .WithMessage(opt=>string.Format(Messages.NotFoundOfficeId));
    }

    //private async Task<bool> ExistsOffice(long officeId, CancellationToken cancellationToken)
    //{

    //}
}