using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.UpdateOffice
{
    public class UpdateOfficeCommand:IRequest<Response>
    {
        public string AccountId { get; set; }
        public long NewOffice { get; set; }

        public UpdateOfficeCommand(string accountId, long newOffice)
        {
            AccountId=accountId;
            NewOffice=newOffice;
        }
    }
}
