using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetAllDoctors;

public class GetAllDoctorsQuery : IRequest<DoctorAllDTO[]>
{
}