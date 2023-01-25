using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Receptionists.UpdateOffice;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Receptionist;

public class UpdateOfficeForReceptionistValidator : AbstractValidator<UpdateOfficeCommand>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly UserManager<Account> _userManager;

    public UpdateOfficeForReceptionistValidator(UserManager<Account> userManager,IOfficeRepository officeRepository)
    {
        _userManager = userManager;
        _officeRepository = officeRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.AccountId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.AccountId));

        RuleFor(x => x.NewOffice)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_officeRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.NewOffice));
    }
   

    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}