using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Command.Receptionists.AddReceptionistRole;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Receptionist;

public class AssignReceptionistRoleValidator : AbstractValidator<AddReceptionistRoleCommand>
{
    private readonly IOfficeRepository _officeRepository;

    public AssignReceptionistRoleValidator(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.ReceptionistDTO.OfficeId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_officeRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.ReceptionistDTO.OfficeId));
    }
}