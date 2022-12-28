using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Update.UpdateAccountIfo
{
    public class UpdateAccountInfoCommand:IRequest<Response>
    {
        public UpdateAccountInfoDTO UserDtO { get; set; }
        public UpdateAccountInfoCommand(UpdateAccountInfoDTO userDtO)
        {
            UserDtO = userDtO;
        }
    }
}
