using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServiceBySpecializationId
{
    public class GetServiceBySpecializationIdQuery:IRequest<OutServicesDto[]>
    {
        public long SpecializationId { get; set; }

        public GetServiceBySpecializationIdQuery(long specializationId)
        {
            SpecializationId=specializationId;
        }
    }
}
