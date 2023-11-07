using Authorization.Application.Query.User;
using Authorization.Application.Resources;
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Validators.Query;

public class LoginValidator : AbstractValidator<LoginQuery>
{
    private readonly UserManager<Account> _userManager;

    public LoginValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .MustAsync(UniqueEmail)
            .WithMessage(opt => string.Format(Messages.NotFoundUser, opt.Email));
    }

    private async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
    {
        //todo: check email 
        var account = await _userManager.FindByEmailAsync(email);
        return account != null;
    }
}