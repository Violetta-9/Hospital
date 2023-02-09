using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Command.Appointment.CancelAppointment
{
    internal class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Response>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public CancelAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public async Task<Response> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            if ( await _appointmentRepository.ExistsAsync(request.AppointmentId, cancellationToken))
            {
              await  _appointmentRepository.DeleteAsync(request.AppointmentId, cancellationToken);
              return Response.Success;
            }
            return Response.Error;
        }
    }
}
