using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorById;

public class GetDoctorByIdQuery : IRequest<DoctorOneDTO>
{
    public long DoctorId { get; set; }

    public GetDoctorByIdQuery(long doctorId)
    {
        DoctorId = doctorId;
    }
}