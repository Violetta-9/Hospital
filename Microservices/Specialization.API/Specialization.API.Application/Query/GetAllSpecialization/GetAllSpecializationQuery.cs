using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Query.GetAllSpecialization
{
    public class GetAllSpecializationQuery : IRequest<SpecializationListDTO[]>
    {

    }
}
