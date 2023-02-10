using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Query.GetAppointmentForDoctor;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Query.Appointment
{
    public class GetAppointmentListForDoctorValidator:AbstractValidator<GetAppointmentForDoctorQuery>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetAppointmentListForDoctorValidator(IDoctorRepository doctorRepository)
        {
            _doctorRepository= doctorRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.DoctorId)
                .MustAsync(_doctorRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundDoctorId, opt.DoctorId));
        }
    }
}
