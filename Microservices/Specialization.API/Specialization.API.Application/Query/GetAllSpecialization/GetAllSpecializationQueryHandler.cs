using Authorization.Data.Repository;
using MediatR;
using Specialization.API.Application.Contracts.Outgoing;

namespace Specialization.API.Application.Query.GetAllSpecialization;

public class GetAllSpecializationQueryHandler : IRequestHandler<GetAllSpecializationQuery, SpecializationListDTO[]>
{
    private readonly ISpecializationRepository _specializationRepository;

    public GetAllSpecializationQueryHandler(ISpecializationRepository specializationRepository)
    {
        _specializationRepository = specializationRepository;
    }

    public async Task<SpecializationListDTO[]> Handle(GetAllSpecializationQuery request,
        CancellationToken cancellationToken)
    {
        return await _specializationRepository.GetAllSpecializationAsync(cancellationToken);
    }
}