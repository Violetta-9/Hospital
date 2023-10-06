using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Command.UpdateSpecializationForServices
{
    public class UpdateSpecializationForServiceCommand:IRequest<Response>
    {
        public long SpecializationId { get; set; }
        public ICollection<long> ServicesId { get; set; }
        public UpdateSpecializationForServiceCommand(long specializationId, ICollection<long> servicesId)
        {
            SpecializationId = specializationId;
            ServicesId = servicesId;
        }
    }
}
