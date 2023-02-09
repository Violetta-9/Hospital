using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Command.Appointment.CreateAppointment;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Command.Appointment
{
    public class CreateAppointmentValidator:AbstractValidator<CreateAppointmentCommand>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IServiceRepository _serviceRepository;

        public CreateAppointmentValidator(IDoctorRepository doctor,IPatientRepository patient,IOfficeRepository office,ISpecializationRepository specialization,IServiceRepository serviceRepository)
        {
            _doctorRepository= doctor;
            _patientRepository= patient;
            _officeRepository= office;
            _specializationRepository= specialization;
            _serviceRepository= serviceRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.AppointmentDto.DoctorId)
                .MustAsync(_doctorRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundDoctorId, opt.AppointmentDto.DoctorId));

            RuleFor(x => x.AppointmentDto.PatientId)
                .MustAsync(_patientRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundPatientId, opt.AppointmentDto.PatientId));

            RuleFor(x => x.AppointmentDto.ServiceId)
                .MustAsync(_serviceRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundServiceId, opt.AppointmentDto.ServiceId));
            
            RuleFor(x => x.AppointmentDto.SpecializationId)
                .MustAsync(_specializationRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundSpecializationId, opt.AppointmentDto.SpecializationId));
            
            RuleFor(x => x.AppointmentDto.OfficeId)
                .MustAsync(_officeRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundOfficeId, opt.AppointmentDto.OfficeId));


        }
    }
}
