using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.API.Application.Contracts.Outgoing;
using Authorization.Data.Repository;
using MediatR;

namespace Appointment.API.Application.Command.AppointmentResult.UpdateAppointmentResult
{
    internal class UpdateAppointmentResultCommandHandler:IRequestHandler<UpdateAppointmentResultCommand,Response>
    {
        private readonly IResultRepository _resultRepository;
        public UpdateAppointmentResultCommandHandler(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<Response> Handle(UpdateAppointmentResultCommand request, CancellationToken cancellationToken)
        {
            var result = await _resultRepository.GetAsync(request.NewResult.AppointmentResultId,cancellationToken);
            if (result != null)
            {
                result.Complaints=request.NewResult.Complaints;
                result.Conclusion=request.NewResult.Conclusion;
                result.Recomendations=request.NewResult.Recomendations;
                await _resultRepository.UpdateAsync(result, cancellationToken);
                return Response.Success;
            }
            return Response.Error;

        }
    }
}
