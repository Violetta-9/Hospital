using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Receptionists.UpdateOffice;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Receptionist;

internal class UpdateOfficeForReceptionistValidator : AbstractValidator<UpdateOfficeCommand>
{
    private readonly UserManager<Account> _userManager;

    public UpdateOfficeForReceptionistValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(Messages.EmptyField)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.AccountId));

        //todo: validation
        //RuleFor(x=>x.NewOffice)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .WithMessage(Messages.EmptyField)
        //    .MustAsync(ExistsOfficeAsync)
        //    .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.NewOffice));
    }

    //private Task<bool> ExistsOfficeAsync(long officeId, CancellationToken arg2)
    //{

    //}

    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}