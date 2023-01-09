using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Patient.GetPatientById;

public class GetPatientByIdQuery : IRequest<PatientOneDTO>
{
    public long PatientId { get; set; }

    public GetPatientByIdQuery(long patientId)
    {
        PatientId = patientId;
    }
}