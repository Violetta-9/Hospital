using Authorization.Application.Command.User.Registration;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Application.Query.User;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Authorization.Application.Resources;

namespace Authorization.Application.Validators.Query
{
    public class LoginValidator: AbstractValidator<LoginQuery>
    {
        private readonly UserManager<Account> _userManager;
        public LoginValidator(UserManager<Account> userManager)
        {
            _userManager = userManager;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .MustAsync(UniqueEmail)
                .WithMessage(opt => string.Format(Messages.NotUniqueEmail, opt.Email));
        }

        private async Task<bool> UniqueEmail(string email, CancellationToken cancellationToken)
        {//todo: check email 
            var account = await _userManager.FindByEmailAsync(email);
            return account != null;
        }
    }
}
