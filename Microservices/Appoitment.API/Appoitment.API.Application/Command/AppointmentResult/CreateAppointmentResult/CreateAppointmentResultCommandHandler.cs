using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using Authorization.Data_Domain.Models;
using MediatR;

namespace Appointment.API.Application.Command.AppointmentResult.CreateAppointmentResult
{
    internal class CreateAppointmentResultCommandHandler : IRequestHandler<CreateAppointmentResultCommand, long>
    {
        private readonly IResultRepository _resultRepository;
        CreateAppointmentResultCommandHandler(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<long> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
        {
            var result = new Result()
            {
                AppointmentId = request.CreateAppointmentDTO.AppointmentId,
                Complaints = request.CreateAppointmentDTO.Complaints,
                Conclusion = request.CreateAppointmentDTO.Conclusion,
                Recomendations = request.CreateAppointmentDTO.Recomendations
            };
           await _resultRepository.InsertAsync(result, cancellationToken);
           return result.Id;
        }
    }
}
