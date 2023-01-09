using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Doctors.UpdateDoctor.UpdateDoctorProfile;

public class UpdateDoctorProfileCommand : IRequest<Response>
{
    public UpdateDoctorDTO DoctorInfo { get; set; }

    public UpdateDoctorProfileCommand(UpdateDoctorDTO doctorInfo)
    {
        DoctorInfo = doctorInfo;
    }
}