using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Receptionists.DeleteReceptionist;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Receptionist;

public class DeleteReceptionistValidator : AbstractValidator<DeleteReceptionistCommand>
{
    private readonly UserManager<Account> _userManager;

    public DeleteReceptionistValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.AccountId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.AccountId));
    }

    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}