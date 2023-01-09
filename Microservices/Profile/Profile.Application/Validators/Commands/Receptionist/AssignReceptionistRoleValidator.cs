using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Receptionists.AddReceptionistRole;

namespace Profile.Application.Validators.Commands.Receptionist;

internal class AssignReceptionistRoleValidator : AbstractValidator<AddReceptionistRoleCommand>
{
    private readonly UserManager<Account> _userManager;

    public AssignReceptionistRoleValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        //todo: validation
        //RuleFor(x=>x.OfficeId)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .WithMessage(Messages.EmptyField)
        //    .MustAsync(ExistsOfficeAsync)
        //    .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.OfficeId));
    }

    //private Task<bool> ExistsOfficeAsync(long officeId, CancellationToken arg2)
    //{

    //}
}