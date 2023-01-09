using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorByOfficeId;

internal class GetDoctorsByOfficeIdQueryHandler : IRequestHandler<GetDoctorsByOfficeIdQuery, DoctorAllDTO[]>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsByOfficeIdQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorAllDTO[]> Handle(GetDoctorsByOfficeIdQuery request, CancellationToken cancellationToken)
    {
        return await _doctorRepository.GetDoctorByOfficeIdAsync(request.OfficeId, cancellationToken);
    }
}