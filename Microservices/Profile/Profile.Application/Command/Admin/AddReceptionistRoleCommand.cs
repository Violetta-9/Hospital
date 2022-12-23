using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Admin
{
    public class AddReceptionistRoleCommand:IRequest<Response>
    {
        public string UserId { get; set; }

        public AddReceptionistRoleCommand(string userId)
        {
            UserId=userId;
        }
    }
}
