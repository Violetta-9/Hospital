using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorByOfficeId;

public class GetDoctorsByOfficeIdQuery : IRequest<DoctorAllDTO[]>
{
    public long OfficeId { get; set; }

    public GetDoctorsByOfficeIdQuery(long officeId)
    {
        OfficeId = officeId;
    }
}