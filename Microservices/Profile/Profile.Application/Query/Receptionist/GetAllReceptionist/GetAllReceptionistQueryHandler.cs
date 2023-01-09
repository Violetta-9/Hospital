using Authorization.Data.Repository;
using MediatR;
using Profile.Application.Contracts.Outgoing;

namespace Profile.Application.Query.Receptionist.GetAllReceptionist;

internal class GetAllReceptionistQueryHandler : IRequestHandler<GetAllReceptionistQuery, ReceptionistAllDTO[]>
{
    private readonly IReceptionistRepository _receptionistRepository;

    public GetAllReceptionistQueryHandler(IReceptionistRepository receptionistRepository)
    {
        _receptionistRepository = receptionistRepository;
    }

    public async Task<ReceptionistAllDTO[]> Handle(GetAllReceptionistQuery request, CancellationToken cancellationToken)
    {
        return await _receptionistRepository.GetAllReceptionistAsync(cancellationToken);
    }
}