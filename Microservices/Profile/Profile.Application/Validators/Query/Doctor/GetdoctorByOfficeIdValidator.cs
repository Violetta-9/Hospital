using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Doctor.GetDoctorByOfficeId;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Doctor
{
    internal class GetDoctorByOfficeIdValidator:AbstractValidator<GetDoctorsByOfficeIdQuery>
    {
      
        public GetDoctorByOfficeIdValidator()
        {
            CreateRules();
        }

        private void CreateRules()
        {
            //todo: validator
            //RuleFor(x => x.OfficeId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .WithMessage(opt => string.Format(Messages.EmptyField, nameof(opt.OfficeId)))
            //    .MustAsync(ExistsOffice)
            //    .WithMessage(opt=>string.Format(Messages.NotFoundOfficeId));
        }

        //private async Task<bool> ExistsOffice(long officeId, CancellationToken cancellationToken)
        //{
            
        //}
    }
}
