using Authorization.Data.Repository;
using FluentValidation;
using Office.Application.Command.UpdateOfficeStatus;
using Office.Application.Resources;

namespace Office.Application.Validators.UpdateOfficeStatus;

public class UpdateOfficeStatusValidator : AbstractValidator<UpdateOfficeStatusCommand>
{
    private readonly IOfficeRepository _officeRepository;

    public UpdateOfficeStatusValidator(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.OfficeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.OfficeId)))
            .MustAsync(ExistsOfficeAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.OfficeId));
    }

    private async Task<bool> ExistsOfficeAsync(long officeId, CancellationToken cancellationToken)
    {
        return await _officeRepository.ExistsAsync(officeId, cancellationToken);
    }
}