using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorProfile;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Doctor;

public class UpdateDoctorProfileValidator : AbstractValidator<UpdateDoctorProfileCommand>
{
    private readonly UserManager<Account> _userManager;

    public UpdateDoctorProfileValidator(UserManager<Account> userManager)
    {
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.DoctorInfo.AccountId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(Messages.EmptyField)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.DoctorInfo.AccountId));
        //todo: validation
        //RuleFor(x=>x.DoctorInfo.OfficeId)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .WithMessage(Messages.EmptyField)
        //    .MustAsync(ExistsOfficeAsync)
        //    .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.DoctorInfo.OfficeId));


        //RuleFor(x => x.DoctorInfo.SpecializationId)
        //    .Cascade(CascadeMode.Stop)
        //    .NotEmpty()
        //    .WithMessage(Messages.EmptyField)
        //    .MustAsync(ExistsSpecializationAsync)
        //    .WithMessage(opt => string.Format(Messages.NotFoundSpecialition, opt.DoctorInfo.SpecializationId));
    }

    //private Task<bool> ExistsSpecializationAsync(long specId, CancellationToken arg2)
    //{

    //}


    //private Task<bool> ExistsOfficeAsync(long officeId, CancellationToken arg2)
    //{

    //}

    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}