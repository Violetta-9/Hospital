using FluentValidation;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorStatus;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Doctor
{
    public class UpdateDoctorStatusValidator: AbstractValidator<UpdateDoctorStatusCommand>
    {
        private readonly UserManager<Account> _userManager;

        public UpdateDoctorStatusValidator(UserManager<Account> userManager)
        {
            _userManager = userManager;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .MustAsync(ExistsAccountAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.AccountId));
            //todo: validation

            //RuleFor(x => x.StatusId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .WithMessage(Messages.EmptyField)
            //    .MustAsync(ExistsStatusAsync)
            //    .WithMessage(opt => string.Format(Messages.NotFoundStatus, opt.StatusId));



        }
        //private Task<bool> ExistsStatusAsync(long statusId, CancellationToken arg2)
        //{

        //}


        private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user != null;
        }
    }
}
