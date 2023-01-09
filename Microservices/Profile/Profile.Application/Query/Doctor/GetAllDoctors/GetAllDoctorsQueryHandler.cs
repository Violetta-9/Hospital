using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetAllDoctors;

public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, DoctorAllDTO[]>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetAllDoctorsQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorAllDTO[]> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {
        return await _doctorRepository.GetAllDoctorsAsync(cancellationToken);
    }
}