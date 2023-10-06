using Authorization.Data.Repository;
using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Query.GetSpecializationById;

public class GetSpecializationByIdQueryHandler : IRequestHandler<GetSpecializationByIdQuery, SpecializationDTO>
{
    private readonly ISpecializationRepository _specializationRepository;

    public GetSpecializationByIdQueryHandler(ISpecializationRepository repository)
    {
        _specializationRepository = repository;
    }

    public async Task<SpecializationDTO> Handle(GetSpecializationByIdQuery request, CancellationToken cancellationToken)
    {
        var specialization =
            await _specializationRepository.GetSpecializationByIdAsync(request.SpecializationId, cancellationToken);

        return specialization;
    }
}