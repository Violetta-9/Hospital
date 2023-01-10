using Authorization.Data.Repository;
using MediatR;
using Office.Application.Contracts.Outgoing;

namespace Office.Application.Query.GetAllOffices;

internal class GetAllOfficesQueryHandler : IRequestHandler<GetAllOfficesQuery, AllOfficesDto[]>
{
    private readonly IOfficeRepository _officeRepository;

    public GetAllOfficesQueryHandler(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<AllOfficesDto[]> Handle(GetAllOfficesQuery request, CancellationToken cancellationToken)
    {
        return await _officeRepository.GetAllOfficesAsync(cancellationToken);
    }
}