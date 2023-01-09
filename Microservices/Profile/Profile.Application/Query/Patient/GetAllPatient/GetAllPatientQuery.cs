using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Patient.GetAllPatient;

public class GetAllPatientQuery : IRequest<PatientAllDTO[]>
{
}