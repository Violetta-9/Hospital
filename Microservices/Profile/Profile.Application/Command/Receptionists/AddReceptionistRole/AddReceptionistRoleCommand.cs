using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.AddReceptionistRole
{
    public class AddReceptionistRoleCommand : IRequest<Response>
    {
        public string UserId { get; set; }
        public long OfficeId { get; set; }

        public AddReceptionistRoleCommand(ReceptionistDTO receptionistDto)
        {
            UserId = receptionistDto.AccountId;
            OfficeId = receptionistDto.OfficeId;
        }
    }
}
