using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Doctor.GetDoctorBySpesializationId;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Doctor
{
    internal class GetDoctorBySpecializationIdValidator:AbstractValidator<GetDoctorsBySpesializationIdQuery>
    {
        
        public GetDoctorBySpecializationIdValidator()
        {
            
            CreateRules();
        }

        private void CreateRules()
        {
            //todo:validator
            //RuleFor(x => x.SpesializationId)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .WithMessage(opt => string.Format(Messages.EmptyField, nameof(opt.SpesializationId)))
            //    .MustAsync(ExistsSpecializationAsync)
            //    .WithMessage(opt=>string.Format(Messages.NotFoundSpecialition,opt.SpesializationId));
        }
        //private Task<bool> ExistsSpecializationAsync(long specId, CancellationToken arg2)
        //{

        //}
    }
}
