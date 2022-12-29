using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorProfile;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorProfile
{
    public class UpdateDoctorProfileCommandHandler : IRequestHandler<UpdateDoctorProfileCommand, Response>
    {
        private readonly IDoctorRepository _doctorRepository;

        public UpdateDoctorProfileCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public async Task<Response> Handle(UpdateDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            var doctor = _doctorRepository.GetDoctorByAccountId(request.DoctorInfo.AccountId, cancellationToken);
            if (doctor == null)
            {
                return Response.Error;
            }
            doctor.OfficeId = request.DoctorInfo.OfficeId;
            doctor.SpecializationId = request.DoctorInfo.SpecializationId;
            doctor.CareerStartYear = new DateTime(request.DoctorInfo.Year, 01, 01);
            await _doctorRepository.UpdateAsync(doctor, cancellationToken);
            return Response.Success;
        }
    }
}
