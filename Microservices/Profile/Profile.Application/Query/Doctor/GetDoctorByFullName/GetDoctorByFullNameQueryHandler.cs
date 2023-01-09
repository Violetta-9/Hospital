using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorByFullName;

public class GetDoctorByFullNameQueryHandler : IRequestHandler<GetDoctorByFullNameQuery, DoctorAllDTO[]>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorByFullNameQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorAllDTO[]> Handle(GetDoctorByFullNameQuery request, CancellationToken cancellationToken)
    {
        return await _doctorRepository.FindDoctorByFullNameAsync(request.DoctorsFullName.FirstName,
            request.DoctorsFullName.LastName, request.DoctorsFullName.MiddleName, cancellationToken);
    }
}