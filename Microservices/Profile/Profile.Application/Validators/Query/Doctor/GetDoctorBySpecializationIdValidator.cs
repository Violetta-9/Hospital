using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Doctor.GetDoctorBySpesializationId;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Doctor;

internal class GetDoctorBySpecializationIdValidator : AbstractValidator<GetDoctorsBySpesializationIdQuery>
{
    private readonly ISpecializationRepository _specializationRepository;
    public GetDoctorBySpecializationIdValidator(ISpecializationRepository specializationRepository)
    {
        _specializationRepository = specializationRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.SpesializationId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsSpecializationAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundSpecialition, opt.SpesializationId));
    }
    private async Task<bool> ExistsSpecializationAsync(long specId, CancellationToken cancellationToken)
    {
        return await _specializationRepository.ExistsAsync(specId, cancellationToken);
    }
}