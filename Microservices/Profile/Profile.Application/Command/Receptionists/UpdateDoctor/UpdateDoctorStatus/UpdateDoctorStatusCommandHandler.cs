using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorStatus
{
    public class UpdateDoctorStatusCommandHandler : IRequestHandler<UpdateDoctorStatusCommand,Response>
    {
        private readonly IDoctorRepository _doctorRepository;

        public UpdateDoctorStatusCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Response> Handle(UpdateDoctorStatusCommand request, CancellationToken cancellationToken)
        {
            var doctor =  _doctorRepository.GetDoctorByAccountId(request.AccounrId, cancellationToken);
            if (doctor == null)
            {
                return Response.Error;
            }
            doctor.Status = request.NewStatus;
            await _doctorRepository.UpdateAsync(doctor, cancellationToken);
            return Response.Success;
        }
    }
}
