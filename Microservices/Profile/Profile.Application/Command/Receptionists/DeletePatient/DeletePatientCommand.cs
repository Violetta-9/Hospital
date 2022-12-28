using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.DeletePatient
{
    public class DeletePatientCommand:IRequest<Response>
    {
        public string AccountId { get; set; }
        public DeletePatientCommand(string accountId)
        {
            AccountId = accountId;
        }
    }
}
