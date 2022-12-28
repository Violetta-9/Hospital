using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.DeleteDoctor
{
    public class DeleteDoctorCommand:IRequest<Response>
    {
        public string AccountId { get; set; }
        public DeleteDoctorCommand(string accountId)
        {
            AccountId = accountId;
        }
    }
}
