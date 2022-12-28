using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.AddDoctorRole
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
