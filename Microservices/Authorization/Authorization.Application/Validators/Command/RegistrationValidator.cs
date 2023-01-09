using Authorization.Application.Command.User.Registration;
using Authorization.Application.Resources;
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Validators.Command;

public class RegistrationValidator : AbstractValidator<RegistrationCommand>
{
    private readonly UserManager<Account> _userManager;

    public RegistrationValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.User.Email)
            .Cascade(CascadeMode.Stop)
            .MustAsync(UniqueEmail)
            .WithMessage(opt => string.Format(Messages.NotUniqueEmail, opt.User.Email));
    }

   

    private async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
    {
        //todo: check email 
        var account = await _userManager.FindByEmailAsync(email);
        return account == null;
    }
}