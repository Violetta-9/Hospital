using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServicesBySpecializationId
{
    public class GetServicesBySpecializationIdQuery : IRequest<OutServicesDto[]>
    {
        public long SpecializationId { get; set; }

        public GetServicesBySpecializationIdQuery(long specializationId)
        {
                SpecializationId=specializationId;
        }
    }
}
