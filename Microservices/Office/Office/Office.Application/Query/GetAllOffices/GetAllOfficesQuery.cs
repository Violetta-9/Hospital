using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Query.GetAllOffices
{
    public class GetAllOfficesQuery : IRequest<AllOfficesDto[]>
    {
    }
}
