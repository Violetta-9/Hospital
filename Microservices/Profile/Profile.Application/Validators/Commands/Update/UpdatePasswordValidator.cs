using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Update.UpdatePassword;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Update;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordCommand>
{
    private readonly UserManager<Account> _userManager;

    public UpdatePasswordValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.NewPassword.AccountId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.NewPassword.AccountId));
    }


    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}