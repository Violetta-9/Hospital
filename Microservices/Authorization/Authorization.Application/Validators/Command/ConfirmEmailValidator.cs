using Authorization.Application.Command.User.ConfirmEmail;
using Authorization.Application.Resources;
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Validators.Command;

internal class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>
{
    private readonly UserManager<Account> _userManager;

    public ConfirmEmailValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.UserId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(Messages.NotEmtyField)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundUser, opt.UserId));

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage(Messages.NotEmptyToken);
    }

    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}