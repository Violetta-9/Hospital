using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorByOfficeId
{
    public class GetDoctorsByOfficeIdQuery : IRequest<DoctorAllDTO[]>
    {
        public long OfficeId { get; set; }

        public GetDoctorsByOfficeIdQuery(long officeId)
        {
            OfficeId = officeId;
        }
    }
}
