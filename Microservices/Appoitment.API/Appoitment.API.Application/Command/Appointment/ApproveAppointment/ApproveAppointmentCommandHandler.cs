using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.ApproveAppointment
{
    internal class ApproveAppointmentCommandHandler : IRequestHandler<ApproveAppointmentCommand, Response>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public ApproveAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Response> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(request.AppointmentId, cancellationToken);
            if (appointment != null)
            {
                appointment.IsApproved=true;
                await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
                return Response.Success;
            }
            return Response.Error;
        }
    }
}
