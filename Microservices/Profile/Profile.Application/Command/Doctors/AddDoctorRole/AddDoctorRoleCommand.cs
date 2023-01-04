using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Doctors.AddDoctorRole
{
    public class AddDoctorRoleCommand:IRequest<Response>
    {
        public DoctorDTO Doctor { get; set; }
        public AddDoctorRoleCommand(DoctorDTO doctor)
        {
            Doctor = doctor;
        }
    }
}
