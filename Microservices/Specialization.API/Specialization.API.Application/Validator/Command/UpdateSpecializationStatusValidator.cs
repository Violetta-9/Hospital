using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using FluentValidation;
using Specialization.API.Application.Command.UpdateSpecializationStatus;
using Specialization.API.Application.Resources;

namespace Specialization.API.Application.Validator.Command
{
    public class UpdateSpecializationStatusValidator:AbstractValidator<UpdateSpecializationStatusCommand>
    {
        private readonly ISpecializationRepository _specializationRepository;
        public UpdateSpecializationStatusValidator(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.UpdateSpecializationStatusDto.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.UpdateSpecializationStatusDto.Id)))
                .MustAsync(SpecializationExist)
                .WithMessage(opt=>string.Format(Messages.NotFoundSpecialization,opt.UpdateSpecializationStatusDto.Id));
        }

        private async Task<bool> SpecializationExist(long id, CancellationToken cancellationToken)
        {
            return await _specializationRepository.ExistsAsync(id, cancellationToken);
        }
    }
}
