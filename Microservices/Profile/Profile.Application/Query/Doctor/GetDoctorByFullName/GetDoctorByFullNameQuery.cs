using MediatR;
using Profile.Application.Contracts.Incoming;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorByFullName
{
    public class GetDoctorByFullNameQuery : IRequest<DoctorAllDTO[]>
    {
        public DoctorsFullNameDTO DoctorsFullName { get; set; }

        public GetDoctorByFullNameQuery(DoctorsFullNameDTO doctorsFullName)
        {
            DoctorsFullName = doctorsFullName;
        }
    }
}
