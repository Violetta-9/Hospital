using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorProfile;
using Profile.Application.Resources;
using System.Threading;

namespace Profile.Application.Validators.Commands.Doctor;

public class UpdateDoctorProfileValidator : AbstractValidator<UpdateDoctorProfileCommand>
{
    private readonly UserManager<Account> _userManager;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IOfficeRepository _officeRepository;

    public UpdateDoctorProfileValidator(UserManager<Account> userManager,ISpecializationRepository specializationRepository,IOfficeRepository officeRepository)
    {
        _specializationRepository = specializationRepository;
        _officeRepository = officeRepository;
        _userManager = userManager;
        CreateRules();
    }

    private void CreateRules()
    {
        RuleFor(x => x.DoctorInfo.AccountId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(ExistsAccountAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.DoctorInfo.AccountId));

        RuleFor(x => x.DoctorInfo.OfficeId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_officeRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.DoctorInfo.OfficeId));


        RuleFor(x => x.DoctorInfo.SpecializationId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_specializationRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundSpecialition, opt.DoctorInfo.SpecializationId));
    }

    private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user != null;
    }
}