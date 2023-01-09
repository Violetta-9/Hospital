using FluentValidation;
using Profile.Application.Query.Doctor.GetDoctorBySpesializationId;

namespace Profile.Application.Validators.Query.Doctor;

internal class GetDoctorBySpecializationIdValidator : AbstractValidator<GetDoctorsBySpesializationIdQuery>
{
    public GetDoctorBySpecializationIdValidator()
    {
        CreateRules();
    }

    private void CreateRules()
    {
        //todo:validator
        //RuleFor(x => x.SpesializationId)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .WithMessage(opt => string.Format(Messages.EmptyField, nameof(opt.SpesializationId)))
        //    .MustAsync(ExistsSpecializationAsync)
        //    .WithMessage(opt=>string.Format(Messages.NotFoundSpecialition,opt.SpesializationId));
    }
    //private Task<bool> ExistsSpecializationAsync(long specId, CancellationToken arg2)
    //{

    //}
}