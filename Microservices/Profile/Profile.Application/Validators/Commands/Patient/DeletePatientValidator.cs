using FluentValidation;
using Profile.Application.Command.Patients.AddPatientRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile.Application.Command.Patients.DeletePatient;
using Authorization.Data_Domain.Models;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Commands.Patient
{
    public class DeletePatientValidator: AbstractValidator<DeletePatientCommand>
    {
        private readonly UserManager<Account> _userManager;

        public DeletePatientValidator(UserManager<Account> userManager)
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


        }


        private async Task<bool> ExistsAccountAsync(string id, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user != null;
        }
    }
}
