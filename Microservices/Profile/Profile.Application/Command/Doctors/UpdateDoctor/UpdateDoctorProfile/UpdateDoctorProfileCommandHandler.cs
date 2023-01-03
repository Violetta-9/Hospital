using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorProfile
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
            var doctor = await _doctorRepository.GetDoctorByAccountIdAsync(request.DoctorInfo.AccountId, cancellationToken);
            if (doctor == null)
            {
                return Response.Error;
            }
            doctor.OfficeId = request.DoctorInfo.OfficeId;
            doctor.SpecializationId = request.DoctorInfo.SpecializationId;
            await _doctorRepository.UpdateAsync(doctor, cancellationToken);
            return Response.Success;
        }
    }
}
