using Authorization.Data.Repository;
using FluentValidation;
using Office.Application.Command.UpdateOffice;
using Office.Application.Resources;

namespace Office.Application.Validators.UpdateOffice;

public class UpdateOfficeValidator : AbstractValidator<UpdateOfficeCommand>
{
    private readonly IOfficeRepository _officeRepository;

    public UpdateOfficeValidator(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.UpdateOfficeDto.OfficeId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.UpdateOfficeDto.OfficeId)))
            .MustAsync(ExistsOfficeAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.UpdateOfficeDto.OfficeId));
    }

    private async Task<bool> ExistsOfficeAsync(long officeId, CancellationToken cancellationToken)
    {
        return await _officeRepository.ExistsAsync(officeId, cancellationToken);
    }
}