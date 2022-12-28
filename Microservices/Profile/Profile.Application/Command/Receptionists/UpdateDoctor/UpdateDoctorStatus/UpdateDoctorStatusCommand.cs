using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Command.Receptionists.UpdateDoctor.UpdateDoctorStatus
{
    public class UpdateDoctorStatusCommand:IRequest<Response>
    {
        public bool NewStatus { get; set; }
        public string AccounrId { get; set; }

        public UpdateDoctorStatusCommand(bool status, string accounrId)
        {
            NewStatus = status;
            AccounrId = accounrId;
        }
    }
}
