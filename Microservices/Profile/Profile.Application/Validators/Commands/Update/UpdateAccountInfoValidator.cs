﻿using FluentValidation;
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
