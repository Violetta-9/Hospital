using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Specialization.API.Application.Command.CreateSpecialization;
using Specialization.API.Application.Resources;

namespace Specialization.API.Application.Validator.Command
{
    public class CreateSpecializationValidator:AbstractValidator<CreateSpecializationCommand>
    {
        public CreateSpecializationValidator()
        {
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.CreateSpecializationDto.Title)
                .NotEmpty()
                .WithMessage(opt=>string.Format(Messages.NotEmptyField,nameof(opt.CreateSpecializationDto.Title)));

            RuleFor(x => x.CreateSpecializationDto.IsActive)
                .NotEmpty()
                .WithMessage(opt => string.Format(Messages.NotEmptyField, nameof(opt.CreateSpecializationDto.IsActive)));
           
        }
    }
}
