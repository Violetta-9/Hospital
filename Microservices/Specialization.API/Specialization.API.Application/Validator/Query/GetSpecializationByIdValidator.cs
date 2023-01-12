using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using FluentValidation;
using Specialization.API.Application.Query.GetSpecializationById;
using Specialization.API.Application.Resources;

namespace Specialization.API.Application.Validator.Query
{
    public class GetSpecializationByIdValidator:AbstractValidator<GetSpecializationByIdQuery>
    {
        private readonly ISpecializationRepository _specializationRepository;
        public GetSpecializationByIdValidator(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.SpecializationId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.SpecializationId)))
                .MustAsync(SpecializationExist)
                .WithMessage(opt => string.Format(Messages.NotFoundSpecialization, opt.SpecializationId));
        }

        private async Task<bool> SpecializationExist(long id, CancellationToken cancellationToken)
        {
            return await _specializationRepository.ExistsAsync(id, cancellationToken);
        }
    }
}
