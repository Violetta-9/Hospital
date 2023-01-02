using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Receptionist.GetAllReceptionist
{
    public class GetAllReceptionistQuery : IRequest<ReceptionistAllDTO[]>
    {
    }
}
