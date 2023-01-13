using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Query.GetSpecializationById
{
    public class GetSpecializationByIdQuery:IRequest<SpecializationDTO>
    {
        public long SpecializationId { get; set; }
        public GetSpecializationByIdQuery(long specializationId)
        {
            SpecializationId = specializationId;
        }
    }
}
