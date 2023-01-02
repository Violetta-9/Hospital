using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Receptionist.GetReceptionistById
{
    public class GetReceptionistByIdQuery:IRequest<ReceptionistOneDTO>
    {
        public long ReceptionistId { get; set; }
        public GetReceptionistByIdQuery(long id)
        {
            ReceptionistId = id;
        }
    }
}
