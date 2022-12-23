using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionist.AddDoctorRole
{
    public class AddDoctorRoleCommand:IRequest<Response>
    {
        public string UserId { get; set; }
        public AddDoctorRoleCommand(string userId)
        {
            UserId = userId;
        }
    }
}
