using Authorization.Data.Repository;
using FluentValidation;
using Specialization.API.Application.Command.UpdateSpecialization;
using Specialization.API.Application.Resources;

namespace Specialization.API.Application.Validator.Command;

public class UpdateSpecializationValidator : AbstractValidator<UpdateSpecializationCommand>
{
    private readonly ISpecializationRepository _specializationRepository;

    public UpdateSpecializationValidator(ISpecializationRepository specializationRepository)
    {
        _specializationRepository = specializationRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.Id)))
            .MustAsync(SpecializationExist)
            .WithMessage(opt => string.Format(Messages.NotFoundSpecialization, opt.Id));

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.Title)));
    }

    private async Task<bool> SpecializationExist(long id, CancellationToken cancellationToken)
    {
        return await _specializationRepository.ExistsAsync(id, cancellationToken);
    }
}