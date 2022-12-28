using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorProfile
{
    public class UpdateDoctorProfileCommand:IRequest<Response>
    {
        public UpdateDoctorDTO DoctorInfo { get; set; }

        public UpdateDoctorProfileCommand(UpdateDoctorDTO doctorInfo)
        {
            DoctorInfo=doctorInfo;
        }
    }
}
