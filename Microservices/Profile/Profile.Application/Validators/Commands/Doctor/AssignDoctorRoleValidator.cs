using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Doctors.AddDoctorRole;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Doctor;

public class AssignDoctorRoleValidator : AbstractValidator<AddDoctorRoleCommand>
{
    private readonly UserManager<Account> _userManager;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IOfficeRepository _officeRepository;


    public AssignDoctorRoleValidator(UserManager<Account> userManager, IStatusRepository statusRepository, IOfficeRepository officeRepository,ISpecializationRepository specializationRepository)
    {
        _userManager = userManager;
        _statusRepository = statusRepository;
        _officeRepository = officeRepository;
        _specializationRepository = specializationRepository;
        CreateRules();

    }

    private void CreateRules()
    {
       
      

        RuleFor(x => x.Doctor.OfficeId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_officeRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.Doctor.OfficeId));

        RuleFor(x => x.Doctor.StatusId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_statusRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundStatus, opt.Doctor.StatusId));

        RuleFor(x => x.Doctor.SpecializationId)
            .Cascade(CascadeMode.Stop)
            .MustAsync(_specializationRepository.ExistsAsync)
            .WithMessage(opt => string.Format(Messages.NotFoundSpecialition, opt.Doctor.SpecializationId));
    }

}