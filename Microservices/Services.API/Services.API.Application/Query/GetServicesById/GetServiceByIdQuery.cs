using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Services.API.Contracts.Outgoing;

namespace Services.API.Application.Query.GetServicesById
{
    public class GetServiceByIdQuery:IRequest<OutServicesDto>
    {
        public long Id { get; set; }
        public GetServiceByIdQuery(long id)
        {
            Id = id;
        }
    }
}
