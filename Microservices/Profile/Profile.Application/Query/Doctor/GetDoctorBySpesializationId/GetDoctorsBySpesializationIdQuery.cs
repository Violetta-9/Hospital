using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorBySpesializationId;

public class GetDoctorsBySpesializationIdQuery : IRequest<DoctorAllDTO[]>
{
    public long SpesializationId { get; set; }

    public GetDoctorsBySpesializationIdQuery(long spesializationId)
    {
        SpesializationId = spesializationId;
    }
}