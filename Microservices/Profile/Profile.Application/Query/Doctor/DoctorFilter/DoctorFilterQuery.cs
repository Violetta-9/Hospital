using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.DoctorFilter
{
    public class DoctorFilterQuery : IRequest<DoctorAllDTO[]>
    {
        public DoctorFilterDTO DoctorFilter { get; set; }

        public DoctorFilterQuery(DoctorFilterDTO doctorFilterDto)
        {
            DoctorFilter= doctorFilterDto;
        }
    }
}
