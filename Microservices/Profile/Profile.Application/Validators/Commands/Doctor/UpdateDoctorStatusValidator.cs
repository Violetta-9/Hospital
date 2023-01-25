using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorStatus;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Doctor;

public class UpdateDoctorStatusValidator : AbstractValidator<UpdateDoctorStatusCommand>
{
    private readonly UserManager<Account> _userManager;
    private readonly IStatusRepository _statusRepository;
    public UpdateDoctorStatusValidator(UserManager<Account> userManager,IStatusRepository statusRepository)
    {
        _userManager = userManager;
        _statusRepository = statusRepository;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.AccountId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.AccountId));
   

        RuleFor(x => x.NewStatus)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_statusRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundStatus, opt.NewStatus));
    }
    
    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}