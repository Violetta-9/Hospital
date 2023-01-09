using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Doctor.GetDoctorBySpesializationId;

internal class
    GetDoctorsBySpesializationIdQueryHandler : IRequestHandler<GetDoctorsBySpesializationIdQuery, DoctorAllDTO[]>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsBySpesializationIdQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DoctorAllDTO[]> Handle(GetDoctorsBySpesializationIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _doctorRepository.GetDoctorBySpecializationIdAsync(request.SpesializationId, cancellationToken);
    }
}