using Authorization.Data_Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Profile.Application.Resources;

using Profile.Application.Command.Receptionists.DeleteReceptionist;

namespace Profile.Application.Validators.Commands.Receptionist
{
    internal class DeleteReceptionistValidator : AbstractValidator<DeleteReceptionistCommand>
    {
        private readonly UserManager<Account> _userManager;

        public DeleteReceptionistValidator(UserManager<Account> userManager)
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
