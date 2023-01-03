using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using FluentValidation;
using Profile.Application.Query.Doctor.GetDoctorById;
using Profile.Application.Resources;

namespace Profile.Application.Validators.Query.Doctor
{
    internal class GetDoctorByIdValidator:AbstractValidator<GetDoctorByIdQuery>
    {
        private readonly IDoctorRepository _dctorRepository;
        public GetDoctorByIdValidator(IDoctorRepository dctorRepository)
        {
            _dctorRepository = dctorRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.DoctorId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage(opt => string.Format(Messages.EmptyField, nameof(opt.DoctorId)))
                .MustAsync(ExistsDoctor)
                .WithMessage(opt=>string.Format(Messages.NotFoundDoctorById,opt.DoctorId));
        }

        private async Task<bool> ExistsDoctor(long doctorId, CancellationToken cancellationToken)
        {
            var doc=await _dctorRepository.GetDoctorByIdAsync(doctorId, cancellationToken);
            return doc != null;
        }
    }
}
