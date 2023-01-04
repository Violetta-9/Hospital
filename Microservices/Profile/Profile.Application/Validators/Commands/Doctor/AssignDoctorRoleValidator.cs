
using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Command.Doctors.AddDoctorRole;
using Profile.Application.Resources
;
namespace Profile.Application.Validators.Commands.Doctor
{
    public class AssignDoctorRoleValidator: AbstractValidator<AddDoctorRoleCommand>
    {
        private readonly UserManager<Account> _userManager;

        public AssignDoctorRoleValidator(UserManager<Account> userManager)
        {
            _userManager=userManager;
            CreateRules();
        }

        private void CreateRules()
        {
            //todo: validation
            //RuleFor(x=>x.Doctor.OfficeId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .WithMessage(Messages.EmptyField)
            //    .MustAsync(ExistsOfficeAsync)
            //    .WithMessage(opt => string.Format(Messages.NotFoundOffice, opt.Doctor.OfficeId));

            //RuleFor(x => x.Doctor.StatusId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .WithMessage(Messages.EmptyField)
            //    .MustAsync(ExistsStatusAsync)
            //    .WithMessage(opt => string.Format(Messages.NotFoundStatus, opt.Doctor.StatusId));

            //RuleFor(x => x.Doctor.SpecializationId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .WithMessage(Messages.EmptyField)
            //    .MustAsync(ExistsSpecializationAsync)
            //    .WithMessage(opt => string.Format(Messages.NotFoundSpecialition, opt.Doctor.SpecializationId));

        }

        //private Task<bool> ExistsSpecializationAsync(long specId, CancellationToken arg2)
        //{
            
        //}

        //private Task<bool> ExistsStatusAsync(long statusId, CancellationToken arg2)
        //{
          
        //}

        //private Task<bool> ExistsOfficeAsync(long officeId, CancellationToken arg2)
        //{
           
        //}

       
    }
}
