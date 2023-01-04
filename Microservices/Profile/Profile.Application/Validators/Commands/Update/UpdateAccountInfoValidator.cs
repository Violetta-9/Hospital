using FluentValidation;
using Profile.Application.Command.Doctors.AddDoctorRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile.Application.Command.Update.UpdateAccountIfo;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Update
{
    public class UpdateAccountInfoValidator: AbstractValidator<UpdateAccountInfoCommand>
    {
        private readonly UserManager<Account> _userManager;
        public UpdateAccountInfoValidator(UserManager<Account> userManager)
        {
            _userManager = userManager;
            CreateRules();
        }
        private void CreateRules()
        {
            RuleFor(x => x.UserDtO.AccountId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(Messages.EmptyField)
                .MustAsync(ExistsAccountAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundAccount, opt.UserDtO.AccountId));

            RuleFor(x => x.UserDtO.Email)
                .Cascade(CascadeMode.Stop)
                .MustAsync(UniqueEmail)
                .WithMessage(opt => string.Format(Messages.NotUniqueEmail, opt.UserDtO.Email));

            RuleFor(x => x.UserDtO.Day)
                .Cascade(CascadeMode.Stop)
                .Must(LimitForTheDay)
                .WithMessage(Messages.NotCorrectDay);


            RuleFor(x => x.UserDtO.Month)
                .Cascade(CascadeMode.Stop)
                .Must(LimitForTheMonth)
                .WithMessage(Messages.NotCorrectMonth);

               
            RuleFor(x => x.UserDtO.Year)
                .Cascade(CascadeMode.Stop)
                .Must(LimitForTheYear)
                .WithMessage(Messages.NotCorrectYear);

        }

        private bool LimitForTheYear(int year)
        {
            return (year > 1900) && (year <= DateTime.Now.Year);
        }

        private bool LimitForTheMonth(int month)
        {
            return (month > 0) && (month <= 12);

        }

        private bool LimitForTheDay(int day)
        {
            return (day > 0) && (day <= 31);
        }

        private async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
        {//todo: check email 
           var account = await _userManager.FindByEmailAsync(email);
           return account != null;
        }

        private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user != null;
        }
    }
}
