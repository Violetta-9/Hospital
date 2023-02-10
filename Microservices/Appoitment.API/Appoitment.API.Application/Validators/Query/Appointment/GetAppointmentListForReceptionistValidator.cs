using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Query.GetAppointmentForReceptionist;
using Appointment.API.Application.Resources;
using Authorization.Data.Repository;
using FluentValidation;

namespace Appointment.API.Application.Validators.Query.Appointment
{
    public class GetAppointmentListForReceptionistValidator:AbstractValidator<GetAppointmentForReceptionistQuery>
    {
        private readonly IOfficeRepository _officeRepository;
        public GetAppointmentListForReceptionistValidator(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
            CreateRule();
        }

        private void CreateRule()
        {
            RuleFor(x => x.OfficeId)
                .MustAsync(_officeRepository.ExistsAsync)
                .WithMessage(opt => string.Format(Messages.NotFoundOfficeId, opt.OfficeId));
        }
    }
}
